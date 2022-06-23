using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Battery
    {        
        public int ID{get; set;}
        public int _amountOfColumns{get; set;}
        public int _amountOfFloors{get; set;}
        public List<Column> columnsList;
        public List<object> floorRequestsButtonsList;
        //public List<int> servedFloors;

        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            
            int ID = _ID;
            //servedFloors = new List<int>();
        //    string status = "online";
            this.columnsList = new List<Column>();
            this.floorRequestsButtonsList = new List<object>();
            if(_amountOfBasements > 0)
            {
                this.createBasementFloorRequestButtons(_amountOfBasements);
                this.createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
                 _amountOfColumns--;
                
            
            
            }
            this.createFloorRequestButtons(_amountOfFloors);
            this.createColumns(_amountOfColumns, _amountOfFloors, _amountOfBasements, _amountOfElevatorPerColumn);
            
            
           
           
           
           

        }
        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            int columnID = 1;
            //List<int> servedFloors = new List<int>();
            int floor = -1;
            Column column = new Column(columnID, "online", _amountOfBasements, _amountOfElevatorPerColumn,  true);
            for(int i=0; i < _amountOfBasements; i++)
            {
                column.servedFloorsList.Add(floor);
                floor--;
            }
            //Column column = new Column(columnID, "online", _amountOfBasements, _amountOfElevatorPerColumn,  true);
            //int cl = Convert.ToInt32(column);
            this.columnsList.Add(column);
            columnID++;

        }
        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            int columnID = 1;
            double calc = _amountOfFloors/_amountOfColumns;
            var amountOfFloorsPerColumn = Math.Round(calc);
            int floor = 1;
            for(int i=0; i < _amountOfColumns; i++)
            {
                //List<int> servedFloors = new List<int>();
                Column column = new Column(columnID, "online", _amountOfFloors, _amountOfElevatorPerColumn,  false);
                for(var x=0; x < amountOfFloorsPerColumn; x++)
                {
                    if(floor <= _amountOfFloors)
                    {
                        column.servedFloorsList.Add(floor);
                        floor++;
                    }
                }
                //Column column = new Column(columnID, "online", _amountOfFloors, _amountOfElevatorPerColumn,  false);
                // int cl = Convert.ToInt32(column);
                this.columnsList.Add(column);
                columnID++;
                

            }
            
        }
        public void createFloorRequestButtons(int _amountOfFloors)
        {
            
            int floorRequestButtonID = 1;
            int buttonFloor = 1;
            
            for(int i=0; i < _amountOfFloors; i++)
            {
                floorRequestButtonID++;
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, "off", buttonFloor, "up");
                //int frb = Convert.ToInt32(floorRequestButton);
                floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor++;
                
            }
            Console.WriteLine("go");
            
            
        }
        
        public void createBasementFloorRequestButtons(int _amountOfBasements)
        {
            int floorRequestButtonID = 1;
            int buttonFloor = -1;
            for(int i=0; i < _amountOfBasements; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, "off", buttonFloor, "down");
                //int frb = Convert.ToInt32(floorRequestButton);
                floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor--;
                floorRequestButtonID++;
            
            }
        }
        public Column findBestColumn(int _requestedFloor)
        {
            Column cl = null;
            foreach(Column column in this.columnsList)
            {
                
                if(column.servedFloorsList.Contains(_requestedFloor))
                {
                    cl =  column;
                }
                
            }
            
            return cl;
            
        }
        public (Column Column, Elevator Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            Column column = this.findBestColumn(_requestedFloor);
            var elevator = column.findElevator(1, _direction);
            elevator.addNewRequest(_requestedFloor);
            elevator.move();
            return (column, elevator);
        }


        // public Column findBestColumn(int _requestedFloor)
        // {
            
        // }
        // //Simulate when a user press a button at the lobby
        // public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        // {
            
        // }
    }
}

