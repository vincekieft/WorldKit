using System.Collections.Generic;
using UnityEngine;
using WorldKit.Procedural.Builders;

namespace WorldKit.Procedural.Layers
{
    public class HydraulicErosion : ALayer<HeightMapBuilder>
    {
        private readonly int _particleCount;
        private readonly int _maxLifeTime;
        private readonly float _inertia;
        private readonly float _startSpeed;
        private readonly float _startWater;
        private readonly float _sedimentCapacityFactor;
        private readonly float _minSedimentCapacity;
        private readonly float _erosionSpeed;
        private readonly float _evaporateSpeed;
        private readonly float _depositSpeed;
        private readonly float _gravity;
        private readonly float _strength;
        private readonly int _brushRadius;
        
        /// <summary>
        /// Hydraulic erosion layer
        /// </summary>
        /// <param name="particleCount">The amount of particles to spawn</param>
        /// <param name="maxLifeTime">The amount of steps a particle can take before being removed</param>
        /// <param name="inertia">The closer inertia gets to 0 the more valley and ravine like structures are formed. When inertia exceeds a certain value ravine like structures are only emerging on steep terrain parts </param>
        /// <param name="startSpeed"></param>
        /// <param name="startWater"></param>
        /// <param name="sedimentCapacityFactor">Determines the amount of sediment a water drop can carry. A higher value results in more sediment being eroded on steeper ground and deposited in lower regions</param>
        /// <param name="minSedimentCapacity"></param>
        /// <param name="erosionSpeed">Erosion speed determines how fast a drop fills with sediment. With high erosion speed a drop quickly files its capacity and after that most likely oly deposits sediment. With a low value the drops pickup sediment for a longer path resulting in stronger ravine formation.</param>
        /// <param name="evaporateSpeed">Evaporate speed determines how fast a drop evaporates. Faster evaporation leads to shorter paths and slower evaporation means longer paths which leads to ravine forming.</param>
        /// <param name="depositSpeed">Deposition speed limits the sediment that is dropped when a drop holds more sediment then it has capacity for</param>
        /// <param name="brushRadius"></param>
        /// <param name="gravity"></param>
        /// <param name="strength"></param>
        public HydraulicErosion(
            int particleCount,
            int maxLifeTime,
            float inertia,
            float startSpeed,
            float startWater,
            float sedimentCapacityFactor,
            float minSedimentCapacity,
            float erosionSpeed,
            float evaporateSpeed,
            float depositSpeed,
            int brushRadius,
            float gravity = 9.81f,
            float strength = 1f)
        {
            _particleCount = particleCount;
            _maxLifeTime = maxLifeTime;
            _inertia = inertia;
            _startSpeed = startSpeed;
            _startWater = startWater;
            _sedimentCapacityFactor = sedimentCapacityFactor;
            _minSedimentCapacity = minSedimentCapacity;
            _erosionSpeed = erosionSpeed;
            _evaporateSpeed = evaporateSpeed;
            _depositSpeed = depositSpeed;
            _brushRadius = brushRadius;
            _gravity = gravity;
            _strength = strength;
        }
        
        public override string Kernel()
        {
            return Constants.ErosionKernel;
        }
        
        public override void SetAttributes(int kernel)
        {
            Shader.SetFloat(Constants.BlendStrengthAttribute, _strength);
            Shader.SetInt(Constants.ErosionMaxLifeTimeAttribute, _maxLifeTime);
            Shader.SetFloat(Constants.ErosionInertiaAttribute, _inertia);
            Shader.SetFloat(Constants.ErosionParticleStartSpeedAttribute, _startSpeed);
            Shader.SetFloat(Constants.ErosionParticleStartWaterAttribute, _startWater);
            Shader.SetFloat(Constants.ErosionSedimentCapacityFactorAttribute, _sedimentCapacityFactor);
            Shader.SetFloat(Constants.ErosionMinSedimentCapacityAttribute, _minSedimentCapacity);
            Shader.SetFloat(Constants.ErosionSpeedAttribute, _erosionSpeed);
            Shader.SetFloat(Constants.ErosionEvaporateSpeedAttribute, _evaporateSpeed);
            Shader.SetFloat(Constants.ErosionDepositSpeedAttribute, _depositSpeed);
            Shader.SetFloat(Constants.ErosionGravityAttribute, _gravity);
            Shader.SetInt(Constants.ErosionBrushRadiusAttribute, _brushRadius);
        }

        public override Vector3 ThreadGroup()
        {
            int threadGroupCount = _particleCount / Constants.GpuGridSize; // TODO ensure that particle count is always in steps of 8 or ensure the shader can handle uneven numbers
            return new Vector3(threadGroupCount,1,1);
        }
    }
}
