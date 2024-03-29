﻿using System;
namespace Commercial_Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            Battery battery = new Battery(1, 4, 60, 6, 5);
            FloorRequestButton floorRequestButton = new FloorRequestButton(1, "idle", 6, "up");
            battery.assignElevator(1, "up");
            Column column = new Column(1, "online", 60, 20, false);
            column.findElevator(12, "up");
            
            int scenarioNumber = 3;
            Scenarios scenarios = new Scenarios();
            scenarios.run(scenarioNumber);
        }
    }
}
