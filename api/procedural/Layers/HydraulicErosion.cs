// Licensed under the Non-Profit Open Software License version 3.0

using UnityEngine;
using WorldKit.api.procedural.Builders;
using WorldKit.api.procedural.Layers.Base;

namespace WorldKit.api.procedural.Layers
{
    /// <summary>
    /// Layer to add hydraulic erosion to the terrain
    /// </summary>
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

        /// <summary>
        /// Hydraulic erosion layer
        /// </summary>
        /// <param name="particleCount">The amount of particles to spawn</param>
        /// <param name="maxLifeTime">The amount of steps a particle can take before being removed</param>
        /// <param name="inertia">The closer inertia gets to 0 the more valley and ravine like structures are formed. When inertia exceeds a certain value ravine like structures are only emerging on steep terrain parts </param>
        /// <param name="startSpeed">The starting speed of the water particles</param>
        /// <param name="startWater">The starting water volume of the water particles. More water means more sediment is carried and takes more time to evaporate</param>
        /// <param name="sedimentCapacityFactor">Determines the amount of sediment a water drop can carry. A higher value results in more sediment being eroded on steeper ground and deposited in lower regions</param>
        /// <param name="minSedimentCapacity"></param>
        /// <param name="erosionSpeed">Erosion speed determines how fast a drop fills with sediment. With high erosion speed a drop quickly files its capacity and after that most likely oly deposits sediment. With a low value the drops pickup sediment for a longer path resulting in stronger ravine formation.</param>
        /// <param name="evaporateSpeed">Evaporate speed determines how fast a drop evaporates. Faster evaporation leads to shorter paths and slower evaporation means longer paths which leads to ravine forming.</param>
        /// <param name="depositSpeed">Deposition speed limits the sediment that is dropped when a drop holds more sediment then it has capacity for</param>
        public HydraulicErosion(
            int particleCount,
            int maxLifeTime,
            float inertia,
            float sedimentCapacityFactor,
            float minSedimentCapacity,
            float erosionSpeed,
            float evaporateSpeed,
            float depositSpeed,
            float startSpeed = 2f,
            float startWater = 1f)
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
            _gravity = Constants.Gravity;
        }
        
        public override string Kernel()
        {
            return Constants.HydraulicErosionKernel;
        }
        
        public override void SetAttributes(int kernel)
        {
            Shader.SetInt(Constants.ErosionParticleCount, _particleCount);
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
        }

        public override Vector3 ThreadGroup()
        {
            int threadGroupCount = _particleCount / Constants.GpuGridSize;
            return new Vector3(threadGroupCount,1,1);
        }
    }
}
