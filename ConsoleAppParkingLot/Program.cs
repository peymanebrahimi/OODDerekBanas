// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

public enum VehicleSize
{
    Motorcycle,
    Compact,
    Large,
}

public abstract class Vehicle
{
    protected List<ParkingSpot> parkingSpots = [];
    protected string LicensePlate { get; }
    protected int SpotsNeeded { get; set; }
    protected VehicleSize VehicleSize { get; set; }

    public void ParkInSpot(ParkingSpot parkingSpot)
    {
        parkingSpots.Add(parkingSpot);
        int[,] ints = new int[4, 2] {
            {1,2},
            {2,3},
            {3,4},
            {4,5},
        };
        int[,] ints1 = new int[2, 4] { { 1, 2, 3, 4 }, { 2, 3, 7, 8 } };

        int[,,] array3D = new int[2, 2, 3]
        {
            { { 1, 2, 3 }, { 4,   5,  6 } },
            { { 7, 8, 9 }, { 10, 11, 12 } }
        };

        int[,,] ints2 =
           {
               {{1,2,3 },{9,8,7 },{8,5,2 },{7,4,1 } },
               {{11,12,13 },{99,88,77 },{ 88,55,22},{77,44,11 } }
           };
    }

    public void ClearSpots()
    {
        foreach (var spot in parkingSpots)
        {
            spot.RemoveVehicle();
        }
        parkingSpots.Clear();
    }

    public abstract bool CanFitInSpot(ParkingSpot spot);
    public abstract void Print();
}

public class ParkingSpot
{
    public Level Level { get; }
    public int Row { get; }
    public int SpotNumber { get; }
    public VehicleSize Size { get; }
    private Vehicle? Vehicle;

    public ParkingSpot(Level level, int row, int spotNumber, VehicleSize size)
    {
        Level = level;
        Row = row;
        SpotNumber = spotNumber;
        Size = size;
    }

    public bool IsAvailable()
    {
        return Vehicle == null;
    }

    public bool CanFitVehicle(Vehicle vehicle)
    {
        return IsAvailable() && vehicle.CanFitInSpot(this);
    }
    public bool Park(Vehicle vehicle)
    {
        if (!CanFitVehicle(vehicle)) return false;

        Vehicle = vehicle;
        vehicle.ParkInSpot(this);
        return true;
    }
    public void RemoveVehicle()
    {
        Level.SpotFreed();
        Vehicle = null;
    }

    public void Print()
    {
        switch (Vehicle)
        {
            case null:
                switch (Size)
                {
                    case VehicleSize.Motorcycle:
                        Console.WriteLine("m");
                        break;
                    case VehicleSize.Compact:
                        Console.WriteLine("c");
                        break;
                    case VehicleSize.Large:
                        Console.WriteLine("l");
                        break;
                }
                break;
            default:
                Vehicle.Print();
                break;
        }
    }
}
public class Level
{
    public void SpotFreed()
    {

    }
}
public class Bus : Vehicle
{
    public Bus()
    {
        SpotsNeeded = 5;
        VehicleSize = VehicleSize.Large;
    }

    public override bool CanFitInSpot(ParkingSpot spot)
    {
        return spot.Size == VehicleSize.Large;
    }

    public override void Print()
    {
        Console.WriteLine("B");
    }
}

public class Car : Vehicle
{
    public Car()
    {
        SpotsNeeded = 1;
        VehicleSize = VehicleSize.Compact;
    }

    public override bool CanFitInSpot(ParkingSpot spot)
    {
        return spot.Size == VehicleSize.Large || spot.Size == VehicleSize.Compact;
    }

    public override void Print()
    {
        Console.WriteLine("C");
    }
}
public class MotorCycle : Vehicle
{
    public MotorCycle()
    {
        SpotsNeeded = 1;
        VehicleSize = VehicleSize.Motorcycle;
    }

    public override bool CanFitInSpot(ParkingSpot spot)
    {
        return true;
    }

    public override void Print()
    {
        Console.WriteLine("M");
    }
}