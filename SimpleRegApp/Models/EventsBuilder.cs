namespace SimpleRegApp.Models
{
    public class EventsBuilder
    {
        private Events _events = new Events();
        
        public EventsBuilder SetEventName(string eventName) 
        {
            _events.EventName = eventName;
            return this;
        }
        public EventsBuilder SetEventDate(DateTime date) 
        {
            _events.Date = date;
            return this;
        }
        public EventsBuilder SetEventDescription(string description) 
        {
            _events.Description = description;
            return this;
        }
        public EventsBuilder SetEventType(string type) 
        {
            _events.Type = type;
            return this;
        }
        public EventsBuilder SetEventFee(double fee) 
        {
            _events.EventFee = fee;
            return this;
        }
        public Events Build() 
        {
            return  _events;
        }
    }
}
