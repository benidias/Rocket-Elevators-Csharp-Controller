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
        public string status;
        public int amountOfFloors;
        public int amountOfElevators;

        
        public Column(int _ID, string _status, int _amountOfFloors,  int _amountOfElevators,  bool _isBasement)
        {
            ID = _ID;
            status = _status;
            amountOfFloors = _amountOfFloors;
            amountOfElevators = _amountOfElevators;
            //List<int> ServedFloors =  servedFloors;
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

        //Simulate when a user press a button at the lobby and send him the right elevator to go up
        public void createElevators(int _amountOfFloors, int _amountOfElevators)
        {
            int elevatorID = 1;
            for(int i=0; i < _amountOfElevators; i++)
            {
                Elevator elevator = new Elevator(elevatorID , "idle", _amountOfFloors, 1);
                
                this.elevatorsList.Add(elevator);
                elevatorID++;
            }
        }

        //Simulate when a user press a button on a floor to go back to the first floor
        public Elevator requestElevator(int userPosition, string direction)
        {
            Elevator elevator= findElevator(userPosition, direction);
            elevator.addNewRequest(userPosition);
            elevator.move();
            elevator.addNewRequest(1);
            elevator.move();
            return elevator;
        }

        // find the right elevator based on where the user want to go
        public Elevator findElevator(int requestedFloor, string requestedDirection)
        {
            
            Elevator bestElevator = elevatorsList[0];
            int bestScore = 5;
            int referenceGap = 1000000;
            
            BestElevatorInformations best = null;
        
            if(requestedFloor > 0)
            {
                
                foreach(Elevator elevator in this.elevatorsList)
                {
                    if(1 == elevator.currentFloor && elevator.status == "stopped")
                    {
                        best = this.checkIfElevatorIsBetter(1, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    } 
                    else if(1 == elevator.currentFloor && elevator.status == "idle")
                    {
                        best = this.checkIfElevatorIsBetter(2, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(1 > elevator.currentFloor && elevator.direction == "up")
                    {
                        best = this.checkIfElevatorIsBetter(3, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(1 < elevator.currentFloor && elevator.direction == "down")
                    {
                        best = this.checkIfElevatorIsBetter(3, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(elevator.status == "idle")
                    {
                        best = this.checkIfElevatorIsBetter(4, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else
                    {
                        best = this.checkIfElevatorIsBetter(5, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    bestElevator = best.bestElevator;
                    bestScore = best.bestScore;
                    referenceGap = best.referenceGap; 
                }              
            }
            else
            {
                foreach(Elevator elevator in this.elevatorsList)
                {
                    if(requestedFloor == elevator.currentFloor && elevator.status == "stopped" && requestedDirection == elevator.direction)
                    {
                        best = this.checkIfElevatorIsBetter(1, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(requestedFloor > elevator.currentFloor && elevator.direction == "up" && requestedDirection == "up")
                    {
                        best = this.checkIfElevatorIsBetter(2, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(requestedFloor < elevator.currentFloor && elevator.direction == "down" && requestedDirection == "down")
                    {
                        best = this.checkIfElevatorIsBetter(2, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else if(elevator.status == "idle")
                    {
                        best = this.checkIfElevatorIsBetter(3, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    else
                    {
                        best = this.checkIfElevatorIsBetter(4, elevator, bestElevator, bestScore, referenceGap, requestedFloor);
                    }
                    bestElevator = best.bestElevator;
                    bestScore = best.bestScore;
                    referenceGap = best.referenceGap;
                }
                
            }
            
            return bestElevator;
        }

        // make sur to make that the elevator selected is the right one
        public BestElevatorInformations checkIfElevatorIsBetter(int scoreToCheck, Elevator newElevator, Elevator bestElevator, int bestScore, int referenceGap, int floor)
        {
            int gap = 0;
            if (scoreToCheck < bestScore) 
            {
                bestScore = scoreToCheck;
                bestElevator = newElevator;
                referenceGap = Math.Abs(newElevator.currentFloor - floor);
            } 
            else if (bestScore == scoreToCheck)
            {
                    
                gap = Math.Abs(newElevator.currentFloor - floor);
                if (referenceGap > gap) {
                    bestElevator = newElevator;
                    referenceGap = gap;
                }
            }
            return new BestElevatorInformations(bestElevator, bestScore, referenceGap);
            
        }

        

    }
    public class BestElevatorInformations
    {
        public Elevator bestElevator{get; set;}
        public int bestScore{get; set;}
        public int referenceGap{get; set;}
        public BestElevatorInformations(Elevator _bestElevator, int _bestScore, int _referenceGap)
        {
            bestElevator = _bestElevator;
            bestScore = _bestScore;
            referenceGap = _referenceGap;
        }
    }
}