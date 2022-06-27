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
        

        // The main battery who contains all 4 columns
        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            
            ID = _ID;
            
       
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
        //creating the column who suppose to served all 6 basement floor
        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            int columnID = 1;
            int floor = -1;
            Column column = new Column(columnID, "online", _amountOfBasements, _amountOfElevatorPerColumn,  true);
            column.servedFloorsList.Add(1);
            for(int i=0; i < _amountOfBasements; i++)
            {
                column.servedFloorsList.Add(floor);
                floor--;
            }
            this.columnsList.Add(column);
            columnID++;

        }
        // creating the 3 other columns who supposed to serve the 60 floors and assign them their elevators
        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            
            double calc = _amountOfFloors/_amountOfColumns;
            var amountOfFloorsPerColumn = Math.Round(calc);
            int floor = 1;
            for(int i=0; i < _amountOfColumns; i++)
            {
                
                int columnID = 1;
                Column column = new Column(columnID, "online", _amountOfFloors, _amountOfElevatorPerColumn,  false);
                column.servedFloorsList.Add(1);
                for(var x=0; x < amountOfFloorsPerColumn; x++)
                {
                    if(floor <= _amountOfFloors)
                    {
                        column.servedFloorsList.Add(floor);
                        floor++;
                    }
                }
                
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
                
                floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor++;
                
            }
            
            
            
        }
        
        public void createBasementFloorRequestButtons(int _amountOfBasements)
        {
            int floorRequestButtonID = 1;
            int buttonFloor = -1;
            for(int i=0; i < _amountOfBasements; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, "off", buttonFloor, "down");
                
                floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor--;
                floorRequestButtonID++;
            
            }
        }

        // function to select what is the column who served the floor requested by user
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

        //Simulate when a user press a button at the lobby and send him the right elevator to go up
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            Column column = this.findBestColumn(_requestedFloor);
            Elevator elevator = column.findElevator(1, _direction);
            elevator.addNewRequest(1);    
            elevator.move();
            elevator.addNewRequest(_requestedFloor);
            elevator.move();
            
            
            return (column, elevator);
        }


       
        
        
    }
}

