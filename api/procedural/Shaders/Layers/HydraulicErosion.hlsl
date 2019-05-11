// Licensed under the Non-Profit Open Software License version 3.0

// Variables
uint ErosionParticleCount;
uint ErosionMaxLifeTime;
float ErosionInertia;
float ErosionParticleStartSpeed;
float ErosionParticleStartWater;
float ErosionSedimentCapacityFactor;
float ErosionMinSedimentCapacity;
float ErosionSpeed;
float ErosionEvaporateSpeed;
float ErosionGravity;
float ErosionDepositSpeed;

// Kernel
[numthreads(32,1,1)]
void HydraulicErosion (uint3 id : SV_DispatchThreadID)
{
    if(id.x < ErosionParticleCount){ // Keep in bounds
        float randomNormalizedX = Random(id.x);
        float randomNormalizedY = Random(id.x + HeightMapResolution * randomNormalizedX);
        
        float2 pos = float2(
            randomNormalizedX * HeightMapResolution,
            randomNormalizedY * HeightMapResolution
        );
        
        // Particle values
        float2 dir = float2(0,0);
        float speed = ErosionParticleStartSpeed;
        float water = ErosionParticleStartWater;
        float sediment = 0;
    
        for(uint lifeTime = 0; lifeTime < ErosionMaxLifeTime; lifeTime++){
            // Calculate texture coords
            uint2 coord = uint2(
                (uint) pos.x,
                (uint) pos.y
            );
            
            int index = PosToIndex(coord, HeightMapResolution);
            
            // Calculate cell offset
            float2 cellOffset = float2(
                pos.x - float(coord.x),
                pos.y - float(coord.y)
            );
            
            // Calculate interpolated gradient and height
            float3 heightAndGradient = CalculateGradientAndHeight(pos, HeightMapResolution, HeightBuffer);
            
            // Update the droplet's direction and position (move position 1 unit regardless of speed)
            dir.x = (dir.x * ErosionInertia - heightAndGradient.x * (1 - ErosionInertia));
            dir.y = (dir.y * ErosionInertia - heightAndGradient.y * (1 - ErosionInertia));
                 
            // Normalize dir
            dir = Normalize2D(dir);
            
            // Add to position
            pos += dir;
            
            // Header guard to check wheter particle is still inside texture and moving
            if((dir.x == 0 && dir.y == 0) || pos.x <= 0 || pos.y <= 0 || pos.x >= float(HeightMapResolution - 2) || pos.y >= float(HeightMapResolution - 2)){ break; }
            
            // Calculate new height and deltaHeight
            float height = CalculateGradientAndHeight(pos, HeightMapResolution, HeightBuffer).z;
            float deltaHeight = height - heightAndGradient.z;
            
            // Calculate the droplet's sediment capacity (higher when moving fast down a slope and contains lots of water)
            float sedimentCapacity = max(-deltaHeight, ErosionMinSedimentCapacity) * speed * water * ErosionSedimentCapacityFactor;
            
            // If carrying more sediment than capacity, or if flowing uphill:
            if (sediment > sedimentCapacity || deltaHeight > 0) {
                // If moving uphill (deltaHeight > 0) try fill up to the current height, otherwise deposit a fraction of the excess sediment
                float amountToDeposit = (deltaHeight > 0) ? 
                    min(deltaHeight, sediment) :
                    (sediment - sedimentCapacity) * ErosionDepositSpeed;
                
                // Remove deposited sediment
                sediment -= amountToDeposit;
                
                // Add the sediment to the four nodes of the current cell using bilinear interpolation
                 AddToBuffer(index, HeightMapResolution, cellOffset, amountToDeposit, HeightBuffer);
            }
            else {
                // Erode a fraction of the droplet's current carry capacity.
                // Clamp the erosion to the change in height so that it doesn't dig a hole in the terrain behind the droplet
                float amountToErode = min((sedimentCapacity - sediment) * ErosionSpeed, -deltaHeight);
                
                // Remove erosion amount from heightmap
                AddToBuffer(index, HeightMapResolution, cellOffset, -amountToErode, HeightBuffer);
                
                // Add eroded amount to sediment currently carried
                sediment += amountToErode;
            }
    
            // Update droplet's speed and water content
            speed = sqrt (max(0,speed * speed + deltaHeight * ErosionGravity));
            water *= (1 - ErosionEvaporateSpeed);
        }
    }
}