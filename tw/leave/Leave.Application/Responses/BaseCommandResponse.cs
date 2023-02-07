namespace Leave.Application.Responses
{
    /// <summary>
    /// Creates a custom reponse with returned types.
    /// </summary>
    public class BaseCommandResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
