namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class CallButton
    {
        public int ID{get; set;}
        public string status{get; set;}
        public int floor{get; set;}
        public string direction{get; set;}
        public CallButton(int _id, int _floor, string _direction)
        {
            int ID = _id;
            //string status = _status;
            int floor = _floor;
            string direction = _direction;
            
        }
    }
}