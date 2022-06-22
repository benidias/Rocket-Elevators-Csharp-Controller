using System;
namespace Commercial_Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            Battery battery = new Battery(1, 4, 60, 6, 5);
            FloorRequestButton floorRequestButton = new FloorRequestButton(1, "idle", 6, "up");
            //Column column = new Column(1, "online", 60, 20, 9, serbe, false);
            
            //int scenarioNumber = Int32.Parse(args[0]);
            // Scenarios scenarios = new Scenarios();
            // scenarios.run(scenarioNumber);
        }
    }
}
