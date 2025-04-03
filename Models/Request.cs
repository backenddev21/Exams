public class Request
    {
        public int FloorNumber { get; }
        public Direction Direction { get; }

        public Request(int floorNumber, Direction direction)
        {
            FloorNumber = floorNumber;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"Request: Floor {FloorNumber}, Direction {Direction}";
        }
    }