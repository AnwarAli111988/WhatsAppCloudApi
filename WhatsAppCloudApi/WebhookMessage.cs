namespace WhatsAppCloudApi
{
    public class WebhookMessage
    {
        public List<Entry>? Entry { get; set; }
    }

    public class Entry
    {
        public List<Change>? Changes { get; set; }
    }

    public class Change
    {
        public Value? Value { get; set; }
    }

    public class Value
    {
        public List<Message>? Messages { get; set; }
    }

    public class Message
    {
        public string? From { get; set; }
        public Text? Text { get; set; }
    }

    public class Text
    {
        public string Body { get; set; }
    }

}
