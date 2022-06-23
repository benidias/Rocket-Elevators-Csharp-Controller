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
        public int ID;
        public int amountOfFloors;
        public Door door;
        public Elevator(int _id, string _status, int _amountOfFloors, int _currentFloor)
        {
            ID = _id;
            status = _status;
            amountOfFloors = _amountOfFloors;
            currentFloor = _currentFloor;
            door = new Door(_id, "closed");
            floorRequestsList = new List<int>();
            completedRequestsList = new List<int>();
            direction = "";
            overweight = false;
        }
       
        public void move()
        {
            //int destination;
            this.status = "moving";
            
            while(this.floorRequestsList.Count > 0)
            {
                int destination = this.floorRequestsList[0];
                
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
                operateDoors();
                completedRequestsList.Add(destination);
                this.floorRequestsList.RemoveAt(0);
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