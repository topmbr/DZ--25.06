// Класс для датчика движения
class MotionSensor
{
    public string Location { get; set; }

    public void DetectMotion()
    {
        // Генерация события при обнаружении движения
        MotionDetectedEvent?.Invoke(this, new MotionDetectedEventArgs(Location, DateTime.Now));
    }

    public event EventHandler<MotionDetectedEventArgs> MotionDetectedEvent;
}

// Класс для аргументов события обнаружения движения
class MotionDetectedEventArgs : EventArgs
{
    public string Location { get; }
    public DateTime Time { get; }

    public MotionDetectedEventArgs(string location, DateTime time)
    {
        Location = location;
        Time = time;
    }
}

// Класс для системы умного дома
class SmartHomeSystem
{
    private List<MotionSensor> motionSensors;

    public SmartHomeSystem()
    {
        motionSensors = new List<MotionSensor>();
    }

    public void RegisterMotionSensor(MotionSensor motionSensor)
    {
        motionSensors.Add(motionSensor);
    }

    public void HandleMotionDetected(object sender, MotionDetectedEventArgs e)
    {
        Console.WriteLine($"Движение обнаружено в {e.Location} в {e.Time}");
    }

    public void StartMonitoring()
    {
        foreach (var motionSensor in motionSensors)
        {
            motionSensor.MotionDetectedEvent += HandleMotionDetected;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        SmartHomeSystem smartHomeSystem = new SmartHomeSystem();

        MotionSensor sensor1 = new MotionSensor { Location = "Living Room" };
        MotionSensor sensor2 = new MotionSensor { Location = "Bedroom" };
        smartHomeSystem.RegisterMotionSensor(sensor1);
        smartHomeSystem.RegisterMotionSensor(sensor2);

        smartHomeSystem.StartMonitoring();

        sensor1.DetectMotion();
        sensor2.DetectMotion();

        // Ждем нажатия клавиши для завершения программы
        Console.ReadKey();
    }
}
