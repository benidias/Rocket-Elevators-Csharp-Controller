using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {
        public List<Elevator> elevatorsList;
        public List<object> callButtonsList;
        public List<int> servedFloorsList;
        public int ID{get; set;}

        
        public Column(int _ID, string _status, int _amountOfFloors,  int _amountOfElevators, List<int> servedFloors, bool _isBasement)
        {
            int ID = _ID;
            string status = _status;
            int amountOfFloors = _amountOfFloors;
            int amountOfElevators = _amountOfElevators;
            List<int> ServedFloors =  servedFloors;
            elevatorsList = new List<Elevator>();
            callButtonsList = new List<object>();
            servedFloorsList = new List<int>();
            this.createElevators(_amountOfFloors, _amountOfElevators);
            this.createCallButtons(_amountOfFloors, _isBasement);
            
            
        }
        public void createCallButtons(int _amountOfFloors, bool _isBasement)
        {
            int callButtonID = 1;
            if(_isBasement == true)
            {
                int buttonFloor = -1;
                for(int i=0; i < _amountOfFloors; i++)
                {
                    CallButton callButton = new CallButton(callButtonID, buttonFloor, "up");
                    //int cb = Convert.ToInt32(callButton);
                    this.callButtonsList.Add(callButton);
                    buttonFloor--;
                    callButtonID++;
                }

            }
            else
            {
                int buttonFloor = 1;
                for(int i=0; i < _amountOfFloors; i++)
                {
                    CallButton callButton = new CallButton(callButtonID, buttonFloor, "down");
                    //int cb = Convert.ToInt32(callButton);
                    this.callButtonsList.Add(callButton);
                    buttonFloor++;
                    callButtonID++;
                }
            }
        }
        public void createElevators(int _amountOfFloors, int _amountOfElevators)
        {
            int elevatorID = 1;
            for(int i=0; i < _amountOfElevators; i++)
            {
                Elevator elevator = new Elevator(elevatorID , "idle", _amountOfFloors, 1);
                //int el = Convert.ToInt32(elevator);
                this.elevatorsList.Add(elevator);
                elevatorID++;
            }
        }
        public Elevator requestElevator(int userPosition, string direction)
        {
            var elevator= this.findElevator(userPosition, direction);
            //elevator.addNewRequest(requestedFloor);
            elevator.move();

            elevator.addNewRequest(1);
            elevator.move();
            return elevator;
        }
        public Elevator findElevator(int requestedFloor, string requestedDirection)
        {
            
            Elevator bestElevator = null;
            int bestScore = 5;
            int referenceGap = 1000000;
            //Elevator bestElevatorInformations = null;
            Elevator bestElevatorInformations = null;
        
            if(requestedFloor == 1)
            {
                
                foreach(Elevator elevator in this.elevatorsList)
                {
                    if(1 == elevator.currentFloor && elevator.status == "stopped")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(1, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(1 == elevator.currentFloor && elevator.status == "idle")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(1 > elevator.currentFloor && elevator.direction == "up")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(1 < elevator.currentFloor && elevator.direction == "down")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(elevator.status == "idle")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(4, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(5, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    bestElevator = bestElevatorInformations;
                    
                }
            }
            else
            {
                foreach(Elevator elevator in this.elevatorsList)
                {
                    if(requestedFloor == elevator.currentFloor && elevator.status == "stopped" && requestedDirection == elevator.direction)
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(1, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(requestedFloor > elevator.currentFloor && elevator.direction == "up" && requestedDirection == "up")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(requestedFloor < elevator.currentFloor && elevator.direction == "down" && requestedDirection == "down")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(2, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(elevator.status == "idle")
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(3, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else
                    {
                        bestElevatorInformations = this.checkIfElevatorIsBetter(4, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    bestElevator = bestElevatorInformations;
                }
            }
            return bestElevator;
        }
        public Elevator checkIfElevatorIsBetter(int scoreToCheck, Elevator newElevator, Elevator bestElevator, int bestScore, int referenceGap, int floor)
        {
            
            if (scoreToCheck < bestScore) 
            {
                bestScore = scoreToCheck;
                bestElevator = newElevator;
                referenceGap = Math.Abs(newElevator.currentFloor - floor);
            } 
            else if (bestScore == scoreToCheck)
            {
                    
                var gap = Math.Abs(newElevator.currentFloor - floor);
                if (referenceGap > gap) {
                    bestElevator = newElevator;
                    referenceGap = gap;
                }
            }
            return bestElevator;
            
        }

        // Simulate when a user press a button on a floor to go back to the first floor
        // public Elevator requestElevator(int userPosition, string direction)
        // {
            
        // }

    }
    public class BestElevatorInformations
    {
        public object bestElevator{get; set;}
        public int bestScore{get; set;}
        public int referenceGap{get; set;}
        public BestElevatorInformations(object _bestElevator, int _bestScore, int _referenceGap)
        {
            bestElevator = _bestElevator;
            bestScore = _bestScore;
            referenceGap = _referenceGap;
        }
    }
}