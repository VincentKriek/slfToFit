﻿using SlfToFit.SlfEntities;

namespace SlfToFit
{
	public class Slf(DateTime fileDate, int revision, Computer computer, GeneralInformation generalInformation, Entry[] entries, Marker[] markers)
	{
		public readonly DateTime FileDate = fileDate;											// Date on which the file was exported
		public readonly int Revision = revision;												// ??
		public readonly Computer Computer = computer;											// Computer entry
		public readonly GeneralInformation GeneralInformation = generalInformation;				// General information entry
		public readonly Entry[] Entries = entries;												// All record entries
		public readonly Marker[] Markers = markers;												// Markers for laps and pauses
		public readonly Marker[] Laps = markers.Where(marker => marker.Type == "l").ToArray();	// Lap markers 
		public readonly Marker[] Pauses = markers.Where(marker => marker.Type == "p").ToArray();// Pause markers

		public ushort NumLaps {  get { return (ushort)Laps.Length; } }

		public float GetLapTimePaused(Marker lap)
		{
			float lower = lap.RelativeStartingTime;
			float upper = lap.RelvativeEndingTime;
			Marker[] pausesInLap = Pauses.Where(pause => pause.RelativeStartingTime >= lower || pause.RelvativeEndingTime <= upper).ToArray();
			return pausesInLap.Aggregate(0f, (acc, marker) =>
			{
				if (marker.RelativeStartingTime < lower)
				{
					return acc + marker.RelvativeEndingTime - lower;
				}
				if (marker.RelvativeEndingTime > upper)
				{
					return acc + upper - marker.RelativeStartingTime;
				}
				return acc + marker.Duration;
			});
		}
	}
}
