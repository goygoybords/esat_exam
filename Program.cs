// TODO 1: Get all meetings that have no overlapping time
using System.Data;

List<Meeting> GetMeetingsWithNoOverlapping(IEnumerable<Meeting> meetings)
{
    List<Meeting> nonOverlappingMeetings = new List<Meeting>();

    foreach (var meeting in meetings)
    {
        bool isOverlapping = false;

        foreach (var otherMeeting in meetings)
        {
            if (meeting != otherMeeting && meeting.Start < otherMeeting.End && meeting.End > otherMeeting.Start)
            {
                isOverlapping = true;
                break;
            }
        }

        if (!isOverlapping)
            nonOverlappingMeetings.Add(meeting);
    }

    return nonOverlappingMeetings;
}

// TODO 2: Get all overlapping meetings from the list
List<Meeting> GetOverlappingMeetings(IEnumerable<Meeting> meetings)
{
    List<Meeting> overlappingMeetings = new List<Meeting>();

    foreach (var meeting in meetings)
    {
        foreach (var otherMeeting in meetings)
        {
            if (meeting != otherMeeting && meeting.Start < otherMeeting.End && meeting.End > otherMeeting.Start)
            {
                if (!overlappingMeetings.Contains(meeting))
                    overlappingMeetings.Add(meeting);
                if (!overlappingMeetings.Contains(otherMeeting))
                    overlappingMeetings.Add(otherMeeting);
            }
        }
    }

    return overlappingMeetings;
}

// TODO 3: Get Previous meeting from meeting list based on the current date and time
Meeting GetPreviousMeeting(IEnumerable<Meeting> meetings)
{
    DateTime now = DateTime.Now;

    var previousMeeting = meetings
        .Where(m => m.End <= now)
        .OrderByDescending(m => m.End)
        .FirstOrDefault();

    if (previousMeeting == null)
    {
        Console.WriteLine("No previous meeting found.");
        return new Meeting();
    }

    return previousMeeting;
}

// TODO 4: Get next meeting from meeting list based on the current date and time
Meeting GetNextMeeting(IEnumerable<Meeting> meetings)
{
    DateTime now = DateTime.Now;

    var nextMeeting = meetings
        .Where(m => m.Start >= now)
        .OrderBy(m => m.Start)
        .FirstOrDefault();

    if (nextMeeting == null)
    {
        Console.WriteLine("No next meeting found.");
        return new Meeting();
    }

    return nextMeeting;
}

#region Ignore Section
// Ignore this section

Random rand = new Random();
List<Meeting> meetings = new List<Meeting>();

// Define a base start time (e.g., today at 8:00 AM)
DateTime baseStart = DateTime.Today.AddHours(8);

for (int i = 0; i < 20; i++)
{
    // Random start time between 0 and 6 hours from the base start time
    DateTime start = baseStart.AddMinutes(rand.Next(0, 360));

    // Random meeting duration between 30 minutes and 2 hours
    DateTime end = start.AddMinutes(rand.Next(30, 120));

    // Add the meeting to the list
    meetings.Add(new Meeting { Start = start, End = end });
}

Console.WriteLine("-------------------------------");
Console.WriteLine("List of Meetings:");
// Print out the meetings
foreach (var meeting in meetings)
{
    Console.WriteLine($"Meeting {meetings.IndexOf(meeting) + 1}: {meeting.Start} - {meeting.End}");
}
#endregion

// Test Lines

var overlappingMeetings = GetOverlappingMeetings(meetings);
Console.WriteLine("-------------------------------");
Console.WriteLine("Overlapping Meetings:");
foreach (var meeting in overlappingMeetings)
{
    Console.WriteLine($"Meeting {meetings.IndexOf(meeting) + 1}: {meeting.Start} - {meeting.End}");
}

Console.WriteLine("-------------------------------");
Console.WriteLine("Meetings with no overlapping:");
var meetingsWithNoOverlapping = GetMeetingsWithNoOverlapping(meetings);
foreach (var meeting in meetingsWithNoOverlapping)
{
    Console.WriteLine($"Meeting {meetings.IndexOf(meeting) + 1}: {meeting.Start} - {meeting.End}");
}

Console.WriteLine("-------------------------------");
Console.WriteLine("Previous Meeting:");
var previousMeeting = GetPreviousMeeting(meetings);
Console.WriteLine($"{previousMeeting.Start} - {previousMeeting.End}");

Console.WriteLine("-------------------------------");
Console.WriteLine("Next Meeting:");
var nextMeeting = GetNextMeeting(meetings);
Console.WriteLine($"{nextMeeting.Start} - {nextMeeting.End}");
