using System.Threading;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Elevator
    {
        public List<int> floorRequestsList;
        public List<int> completedRequestsList;
        public int currentFloor;
        public int screenDisplay;
        public string status;
        public string direction;
        public bool overweight;
        public int ID{get; set;}
        public Elevator(int _id, string _status, int _amountOfFloors, int _currentFloor)
        {
            int ID = _id;
            string status = _status;
            int amountOfFloors = _amountOfFloors;
            int currentFloor = _currentFloor;
            Door door = new Door(_id, "closed");
            this.floorRequestsList = new List<int>();
            completedRequestsList = new List<int>();
            direction = "null";
            overweight = false;
            this.move();
            this.sortFloorList();
            //this.addNewRequest(requestedFloor);
        }
        
        public void move()
        {
            int destination;
            while(this.floorRequestsList.Count > 0)
            {
                destination = this.floorRequestsList[0];
                this.status = "moving";
                if(this.currentFloor < destination)
                {
                    this.direction = "up";
                    this.sortFloorList();
                    destination = this.floorRequestsList[0];
                    while(this.currentFloor < destination)
                    {
                        this.currentFloor++;
                        this.screenDisplay = this.currentFloor;
                    }
                }
                else if(this.currentFloor > destination)
                {
                    this.direction = "down";
                    this.sortFloorList();
                    destination = this.floorRequestsList[0];
                    while(this.currentFloor > destination)
                    {
                        this.currentFloor--;
                        this.screenDisplay = this.currentFloor;
                    }
                }
                this.status = "stopped";
                this.operateDoors();
                completedRequestsList.Add(destination);
                floorRequestsList.RemoveAt(0);
            }
            this.status = "idle";
            
        }
        public void sortFloorList()
        {
            if(this.direction == "up"){
                this.floorRequestsList.Sort();
            }
            else
            {
                this.floorRequestsList.Sort();
                this.floorRequestsList.Reverse();
            }
        }
        public void operateDoors()
        {
            Door door = new Door(1, "idle");
            door.status = "opened";

            if(overweight == false)
            {
                door.status = "closing";
                if(this.status != "obstruction")
                {
                    door.status = "closed";
                }
                else
                {
                    this.operateDoors();
                }
            }
            else
            {
                while(overweight == true)
                {
                    overweight=true;
                }
                this.operateDoors();
            }
        }

        public void addNewRequest(int requestedFloor)
        {
            if((this.floorRequestsList.Contains(requestedFloor)) == false)
            {
                this.floorRequestsList.Add(requestedFloor);
            }
            if(this.currentFloor < requestedFloor)
            {
                this.direction = "up";
            }
            if(this.currentFloor > requestedFloor)
            {
                this.direction = "down";
            }
        }
        
        
    }
}