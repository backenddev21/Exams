namespace elevator
{
    public class PassengerRequest
    {
        public int ToFloor { get; set; }
        public int FromFloor { get; set; }
        public Direction Direction { get; set; }

        public PassengerRequest(int toFloor, int fromFloor, Direction going)
        {
            ToFloor = toFloor;
            FromFloor = fromFloor;
            Direction = going;
        }

        public override string ToString()
        {
            return $"Request: From floor {FromFloor} to floor {ToFloor}, Direction is going {Direction}.";
        }
    }
}
