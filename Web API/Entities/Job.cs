using System;
namespace Web_API.Entities
{
	public class Job
	{
        public static object Jobs { get; internal set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Organisation { get; set; }
        public string Email { get; set; }
    }
}

