using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;


namespace EntityTest
{
    public class EntityTest : Resource
    {
        const uint vehicleHash = (uint)VehicleModel.Elegy;
        static Random rnd = new Random();
        
        public static float toRadians(float val)
        {
            return (float)((float)val/180) * (float)Math.PI;
        }

        public static Rgba rndRGB()
        {
            return new Rgba((byte)rnd.NextInt64(0, 255), (byte)rnd.NextInt64(0, 255), (byte)rnd.NextInt64(0, 255), 0);
        }

        public override void OnStart()
        {
            Alt.Log("Starting spawning Vehicles");

            int i = 1;
            int count = SpawnPositions.VehicleList.Count;
            foreach(var kv in SpawnPositions.VehicleList)
            {
                var pos = kv.Key;
                var rotDeg = kv.Value;

                var rot = new Position(toRadians(rotDeg.Roll), toRadians(rotDeg.Pitch), toRadians(rotDeg.Yaw));
                var veh = Alt.CreateVehicle(vehicleHash, pos, rot);
                veh.PrimaryColorRgb = rndRGB();
                veh.SecondaryColorRgb = rndRGB();
                veh.NumberplateText = "#" + i;

                i++; 
            }
            Alt.Log($"Finished spawning {count} vehicles.");

            Alt.OnPlayerConnect += OnPlayerConnect;
        }

        public override void OnStop()
        {

        }

        public void OnPlayerConnect(IPlayer player, string reason)
        {
            var spawnPos = new Position(1, 1, 72);
            player.Model = Alt.Hash("A_M_M_Salton_02");
            player.Spawn(spawnPos);

            var vehicle = Alt.CreateVehicle(vehicleHash, spawnPos, new Position(0, 0, 0));
            vehicle.PrimaryColorRgb = new Rgba(255, 0, 0, 0);
            vehicle.SecondaryColorRgb = new Rgba(255, 0, 0, 0);
            player.SetIntoVehicle(vehicle, 1);
        }
    }
}


