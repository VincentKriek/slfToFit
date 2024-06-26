﻿using System.Text.Json;
using System.Xml.Linq;
using SlfToFit.SlfEntities;

namespace SlfToFit
{
	public static class SlfParser
	{
		public static Slf? ParseSlf(string filePath, LoggingService loggingService)
		{
			try
			{
				loggingService.WriteInfoLine("Load xml file");
				XDocument slf = XDocument.Load(filePath);

				loggingService.WriteInfoLine("Retrieving elements from xml:");
				loggingService.WriteInfoLine("	Activity");
				XElement activityElement = GetRootElement(slf);
				loggingService.WriteInfoLine("	Computer");
				XElement computerElement = GetElement(activityElement, "Computer");
				loggingService.WriteInfoLine("	GeneralInformation");
				XElement generalInformationElement = GetElement(activityElement, "GeneralInformation");
				loggingService.WriteInfoLine("	Entries");
				XElement entriesElement = GetElement(activityElement, "Entries");
				loggingService.WriteInfoLine("	Markers");
				XElement markerElement = GetElement(activityElement, "Markers");
				loggingService.WriteInfoLine("Successfully retrieved xml elements");

				loggingService.WriteInfoLine("Parsing retrieved xml fields:");
				loggingService.WriteInfoLine("	Filedate");
				DateTime fileDate = GlobalUtilities.SlgDateToDateTime(GetAttribute(activityElement, "fileDate").Value);
				loggingService.WriteInfoLine("	Revision");
				int revision = int.Parse(GetAttribute(activityElement, "revision").Value);
				loggingService.WriteInfoLine("	Computer");
				Computer computer = XmlToComputer(computerElement);
				loggingService.WriteInfoLine("	GeneralInformation");
				GeneralInformation generalInformation = XmlToGeneralInformation(generalInformationElement);
				loggingService.WriteInfoLine("	Entries");
				Entry[] entries = XmlToEntries(entriesElement);
				loggingService.WriteInfoLine("	Marker");
				Marker[] markers = XmlToMarkers(markerElement);
				loggingService.WriteInfoLine("Successfully parsed retrieved xml fields");
				return new Slf(fileDate, revision, computer, generalInformation, entries, markers);
			}
			catch(FileNotFoundException ex)
			{
				loggingService.WriteErrorLine("FileNotFoundException:");
				loggingService.WriteErrorLine(ex);
			}
			catch(InvalidOperationException ex)
			{
				loggingService.WriteErrorLine("InvalidOperationException:");
				loggingService.WriteErrorLine(ex);
			}
			catch (Exception ex)
			{
				loggingService.WriteErrorLine("Exception:");
				loggingService.WriteErrorLine(ex);
			}
			// if try catch fails return null
			return null;
		}

		private static Computer XmlToComputer(XElement computer) 
		{
			string unit = GetAttribute(computer, "unit").Value;
			string serial = GetAttribute(computer, "serial").Value;
			string activityType = GetAttribute(computer, "activityType").Value;
			DateTime dateCode = GlobalUtilities.SlgDateToDateTime(GetAttribute(computer, "dateCode").Value);
			return new Computer(unit, serial, activityType, dateCode);
		}

		private static GeneralInformation XmlToGeneralInformation(XElement generalInformation)
		{
			User user = XmlToUser(GetElement(generalInformation, "user"));
			string sport = GetElement(generalInformation, "sport").Value;
			string guid = GetElement(generalInformation, "GUID").Value;
			int age = int.Parse(GetElement(generalInformation, "age").Value);
			ushort altitudeDifferencesDownhill = (ushort)(int.Parse(GetElement(generalInformation, "altitudeDifferencesDownhill").Value)/1000);
			ushort altitudeDifferencesUphill = (ushort)(int.Parse(GetElement(generalInformation, "altitudeDifferencesUphill").Value)/1000);
			int averageAltitude = int.Parse(GetElement(generalInformation, "averageAltitude").Value);
			byte averageCadence = byte.Parse(GetElement(generalInformation, "averageCadence").Value);
			byte averageHeartrate = byte.Parse(GetElement(generalInformation, "averageHeartrate").Value);
			float averageInclineUphill = float.Parse(GetElement(generalInformation, "averageInclineUphill").Value);
			float averageInclineDownhill = float.Parse(GetElement(generalInformation, "averageInclineDownhill").Value);
			ushort averagePower = ushort.Parse(GetElement(generalInformation, "averagePower").Value);
			float averagePowerWattPerKg = float.Parse(GetElement(generalInformation, "averagePowerWattPerKG").Value);
			float averageRiseRate = float.Parse(GetElement(generalInformation, "averageRiseRate").Value);
			float averageRiseRateUphill = float.Parse(GetElement(generalInformation, "averageRiseRateUphill").Value);
			float averageRiseRateDownhill = float.Parse(GetElement(generalInformation, "averageRiseRateDownhill").Value);
			float averageSpeed = float.Parse(GetElement(generalInformation, "averageSpeed").Value);
			float averageSpeedDownhill = float.Parse(GetElement(generalInformation, "averageSpeedDownhill").Value);
			float averageSpeedUphill = float.Parse(GetElement(generalInformation, "averageSpeedUphill").Value);
			float averageTemperature = float.Parse(GetElement(generalInformation, "averageTemperature").Value);
			string bike = GetElement(generalInformation, "bike").Value;
			int bikeWeight = int.Parse(GetElement(generalInformation, "bikeWeight").Value);
			int bodyHeight = int.Parse(GetElement(generalInformation, "bodyHeight").Value);
			int bodyWeight = int.Parse(GetElement(generalInformation, "bodyWeight").Value);
			bool calibration = bool.Parse(TryGetElement(generalInformation, "calibration")?.Value ?? "false");
			ushort calories = ushort.Parse(GetElement(generalInformation, "calories").Value);
			string dataType = GetElement(generalInformation, "dataType").Value;
			string description = GetElement(generalInformation, "description").Value;
			float distance = float.Parse(GetElement(generalInformation, "distance").Value);
			float distanceDownhill = float.Parse(GetElement(generalInformation, "distanceDownhill").Value);
			float distanceUphill = float.Parse(GetElement(generalInformation, "distanceUphill").Value);
			int exerciseTime = int.Parse(GetElement(generalInformation, "exerciseTime").Value);
			string externalLink = GetElement(generalInformation, "externalLink").Value;
			int hrMax = int.Parse(GetElement(generalInformation, "hrMax").Value);
			int intensityZone1Start = int.Parse(GetElement(generalInformation, "intensityZone1Start").Value);
			int intensityZone2Start = int.Parse(GetElement(generalInformation, "intensityZone2Start").Value);
			int intensityZone3Start = int.Parse(GetElement(generalInformation, "intensityZone3Start").Value);
			int intensityZone4Start = int.Parse(GetElement(generalInformation, "intensityZone4Start").Value);
			int intensityZone4End = int.Parse(GetElement(generalInformation, "intensityZone4End").Value);
			float latitudeEnd = float.Parse(GetElement(generalInformation, "latitudeEnd").Value);
			float latitudeStart = float.Parse(GetElement(generalInformation, "latitudeStart").Value);
			int linkedRouteId = int.Parse(GetElement(generalInformation, "linkedRouteId").Value);
			int logVersion = int.Parse(TryGetElement(generalInformation, "logVersion")?.Value ?? "400");
			float longitudeEnd = float.Parse(GetElement(generalInformation, "longitudeEnd").Value);
			float longitudeStart = float.Parse(GetElement(generalInformation, "longitudeStart").Value);
			float manualTemperature = float.Parse(GetElement(generalInformation, "manualTemperature").Value);
			int maximumAltitude = int.Parse(GetElement(generalInformation, "maximumAltitude").Value);
			byte maximumCadence = byte.Parse(GetElement(generalInformation, "maximumCadence").Value);
			byte maximumHeartrate = byte.Parse(GetElement(generalInformation, "maximumHeartrate").Value);
			float maximumIncline = float.Parse(GetElement(generalInformation, "maximumIncline").Value);
			float maximumInclineDownhill = float.Parse(GetElement(generalInformation, "maximumInclineDownhill").Value);
			float maximumInclineUphill = float.Parse(GetElement(generalInformation, "maximumInclineUphill").Value);
			ushort maximumPower = ushort.Parse(GetElement(generalInformation, "maximumPower").Value);
			float maximumRiseRate = float.Parse(GetElement(generalInformation, "maximumRiseRate").Value);
			float maximumSpeed = float.Parse(GetElement(generalInformation, "maximumSpeed").Value);
			float maximumTemperature = float.Parse(GetElement(generalInformation, "maximumTemperature").Value);
			int minimumAltitude = int.Parse(GetElement(generalInformation, "minimumAltitude").Value);
			byte minimumCadence = byte.Parse(GetElement(generalInformation, "minimumCadence").Value);
			byte minimumHeartrate = byte.Parse(GetElement(generalInformation, "minimumHeartrate").Value);
			float minimumIncline = float.Parse(GetElement(generalInformation, "minimumIncline").Value);
			int minimumPower = int.Parse(GetElement(generalInformation, "minimumPower").Value);
			float minimumRiseRate = float.Parse(GetElement(generalInformation, "minimumRiseRate").Value);
			float minimumSpeed = float.Parse(GetElement(generalInformation, "minimumSpeed").Value);
			float minimumTemperature = float.Parse(GetElement(generalInformation, "minimumTemperature").Value);
			string name = GetElement(generalInformation, "name").Value;
			int pauseTime = int.Parse(GetElement(generalInformation, "pauseTime").Value);
			int powerZone1Start = int.Parse(GetElement(generalInformation, "powerZone1Start").Value);
			int powerZone2Start = int.Parse(GetElement(generalInformation, "powerZone2Start").Value);
			int powerZone3Start = int.Parse(GetElement(generalInformation, "powerZone3Start").Value);
			int powerZone4Start = int.Parse(GetElement(generalInformation, "powerZone4Start").Value);
			int powerZone5Start = int.Parse(GetElement(generalInformation, "powerZone5Start").Value);
			int powerZone6Start = int.Parse(GetElement(generalInformation, "powerZone6Start").Value);
			int powerZone7Start = int.Parse(GetElement(generalInformation, "powerZone7Start").Value);
			int powerZone7End = int.Parse(GetElement(generalInformation, "powerZone7End").Value);
			int rating = int.Parse(GetElement(generalInformation, "rating").Value);
			int feeling = int.Parse(GetElement(generalInformation, "feeling").Value);
			int trainingTimeDownhill = int.Parse(GetElement(generalInformation, "trainingTimeDownhill").Value);
			int trainingTimeUphill = int.Parse(GetElement(generalInformation, "trainingTimeUphill").Value);
			int samplingRate = int.Parse(GetElement(generalInformation, "samplingRate").Value);
			int shoulderWidth = int.Parse(GetElement(generalInformation, "shoulderWidth").Value);
			DateTime startDate = GlobalUtilities.SlgDateToDateTime(GetElement(generalInformation, "startDate").Value);
			bool statistic = bool.Parse(GetElement(generalInformation, "statistic").Value);
			int timeInIntensityZone1 = int.Parse(GetElement(generalInformation, "timeInIntensityZone1").Value);
			int timeInIntensityZone2 = int.Parse(GetElement(generalInformation, "timeInIntensityZone2").Value);
			int timeInIntensityZone3 = int.Parse(GetElement(generalInformation, "timeInIntensityZone3").Value);
			int timeInIntensityZone4 = int.Parse(GetElement(generalInformation, "timeInIntensityZone4").Value);
			int timeInPowerZone1 = int.Parse(GetElement(generalInformation, "timeInPowerZone1").Value);
			int timeInPowerZone2 = int.Parse(GetElement(generalInformation, "timeInPowerZone2").Value);
			int timeInPowerZone3 = int.Parse(GetElement(generalInformation, "timeInPowerZone3").Value);
			int timeInPowerZone4 = int.Parse(GetElement(generalInformation, "timeInPowerZone4").Value);
			int timeInPowerZone5 = int.Parse(GetElement(generalInformation, "timeInPowerZone5").Value);
			int timeInPowerZone6 = int.Parse(GetElement(generalInformation, "timeInPowerZone6").Value);
			int timeInPowerZone7 = int.Parse(GetElement(generalInformation, "timeInPowerZone7").Value);
			int timeOverIntensityZone = int.Parse(GetElement(generalInformation, "timeOverIntensityZone").Value);
			int timeUnderIntensityZone = int.Parse(GetElement(generalInformation, "timeUnderIntensityZone").Value);
			int trackProfile = int.Parse(GetElement(generalInformation, "trackProfile").Value);
			int trainingTime = int.Parse(GetElement(generalInformation, "trainingTime").Value);
			int unitId = int.Parse(GetElement(generalInformation, "unitId").Value);
			int weather = int.Parse(GetElement(generalInformation, "weather").Value);
			int wheelSize = int.Parse(GetElement(generalInformation, "wheelSize").Value);
			int wind = int.Parse(GetElement(generalInformation, "wind").Value);
			uint workInKJ = uint.Parse(GetElement(generalInformation, "workInKJ").Value);
			float best5KTime = float.Parse(GetElement(generalInformation, "best5KTime").Value);
			float best5KEntry = float.Parse(GetElement(generalInformation, "best5KEntry").Value);
			float best20MinPower = float.Parse(GetElement(generalInformation, "best20minPower").Value);
			float best20MinPowerEntry = float.Parse(GetElement(generalInformation, "best20minPowerEntry").Value);
			float powerNP = float.Parse(GetElement(generalInformation, "powerNP").Value);
			float powerTSS = float.Parse(GetElement(generalInformation, "powerTSS").Value);
			float powerFTP = float.Parse(GetElement(generalInformation, "powerFTP").Value);
			int pedalingTime = int.Parse(GetElement(generalInformation, "pedalingTime").Value);
			float pedalingIndex = float.Parse(GetElement(generalInformation, "pedalingIndex").Value);
			float averageBalanceRight = float.Parse(GetElement(generalInformation, "averageBalanceRight").Value);
			float averageBalanceLeft = float.Parse(GetElement(generalInformation, "averageBalanceLeft").Value);
			float powerIF = float.Parse(GetElement(generalInformation, "powerIF").Value);
			float torqueEffectLeft = float.Parse(GetElement(generalInformation, "torqueEffectLeft").Value);
			float torqueEffectRight = float.Parse(GetElement(generalInformation, "torqueEffectRight").Value);
			float pedalSmoothLeft = float.Parse(GetElement(generalInformation, "pedalSmoothLeft").Value);
			float pedalSmoothRight = float.Parse(GetElement(generalInformation, "pedalSmoothRight").Value);
			float averageCadenceCalc = float.Parse(GetElement(generalInformation, "averageCadenceCalc").Value);
			float averagePowerCalc = float.Parse(GetElement(generalInformation, "averagePowerCalc").Value);
			string[] activityStatus = GetElement(generalInformation, "activityStatus").Value.Split(',');
			bool activityTrackerDayComplete = bool.Parse(TryGetElement(generalInformation, "activityTrackerDayComplete")?.Value ?? "false");
			Dictionary<string, ulong> sharingInfo = JsonSerializer.Deserialize<Dictionary<string, string>>(GetElement(generalInformation, "sharingInfo").Value)?.ToDictionary(kvp => kvp.Key, kvp => ulong.Parse(kvp.Value)) ?? [];
			return new GeneralInformation(
				user,
				sport,
				guid,
				age,
				altitudeDifferencesDownhill,
				altitudeDifferencesUphill,
				averageAltitude,
				averageCadence,
				averageHeartrate,
				averageInclineUphill,
				averageInclineDownhill,
				averagePower,
				averagePowerWattPerKg,
				averageRiseRate,
				averageRiseRateUphill,
				averageRiseRateDownhill,
				averageSpeed,
				averageSpeedDownhill,
				averageSpeedUphill,
				averageTemperature,
				bike,
				bikeWeight,
				bodyHeight,
				bodyWeight,
				calibration,
				calories,
				dataType,
				description,
				distance,
				distanceDownhill,
				distanceUphill,
				exerciseTime,
				externalLink,
				hrMax,
				intensityZone1Start,
				intensityZone2Start,
				intensityZone3Start,
				intensityZone4Start,
				intensityZone4End,
				latitudeEnd,
				latitudeStart,
				linkedRouteId,
				logVersion,
				longitudeEnd,
				longitudeStart,
				manualTemperature,
				maximumAltitude,
				maximumCadence,
				maximumHeartrate,
				maximumIncline,
				maximumInclineDownhill,
				maximumInclineUphill,
				maximumPower,
				maximumRiseRate,
				maximumSpeed,
				maximumTemperature,
				minimumAltitude,
				minimumCadence,
				minimumHeartrate,
				minimumIncline,
				minimumPower,
				minimumRiseRate,
				minimumSpeed,
				minimumTemperature,
				name,
				pauseTime,
				powerZone1Start,
				powerZone2Start,
				powerZone3Start,
				powerZone4Start,
				powerZone5Start,
				powerZone6Start,
				powerZone7Start,
				powerZone7End,
				rating,
				feeling,
				trainingTimeDownhill,
				trainingTimeUphill,
				samplingRate,
				shoulderWidth,
				startDate,
				statistic,
				timeInIntensityZone1,
				timeInIntensityZone2,
				timeInIntensityZone3,
				timeInIntensityZone4,
				timeInPowerZone1,
				timeInPowerZone2,
				timeInPowerZone3,
				timeInPowerZone4,
				timeInPowerZone5,
				timeInPowerZone6,
				timeInPowerZone7,
				timeOverIntensityZone,
				timeUnderIntensityZone,
				trackProfile,
				trainingTime,
				unitId,
				weather,
				wheelSize,
				wind,
				workInKJ,
				best5KTime,
				best5KEntry,
				best20MinPower,
				best20MinPowerEntry,
				powerNP,
				powerTSS,
				powerFTP,
				pedalingTime,
				pedalingIndex,
				averageBalanceRight,
				averageBalanceLeft,
				powerIF,
				torqueEffectLeft,
				torqueEffectRight,
				pedalSmoothLeft,
				pedalSmoothRight,
				averageCadenceCalc,
				averagePowerCalc,
				activityStatus,
				activityTrackerDayComplete,
				sharingInfo);
		}
		private static Entry[] XmlToEntries(XElement entries)
		{
			if (!entries.HasElements)
			{
				throw new ArgumentException("The log file has no entries");
			}
			IEnumerable<XElement> children = entries.Elements();
			return children.Select(XmlToEntry).ToArray();
		}
		private static Marker[] XmlToMarkers(XElement markers)
		{
			if (!markers.HasElements)
			{
				throw new ArgumentException("The log file has no markers");
			}
			IEnumerable<XElement> children = markers.Elements();
			return children.Select(XmlToMarker).ToArray();
		}

		private static Entry XmlToEntry(XElement entry)
		{
			int altitude = int.Parse(GetAttribute(entry, "altitude").Value);
			int altitudeDifferencesDownhill = int.Parse(GetAttribute(entry, "altitudeDifferencesDownhill").Value);
			int altitudeDifferencesUphill = int.Parse(GetAttribute(entry, "altitudeDifferencesUphill").Value);
			float cadence = float.Parse(GetAttribute(entry, "cadence").Value);
			float calories = float.Parse(GetAttribute(entry, "calories").Value);
			float distance = float.Parse(GetAttribute(entry, "distance").Value);
			float distanceAbsolute = float.Parse(GetAttribute(entry, "distanceAbsolute").Value);
			float distanceDownhill = float.Parse(GetAttribute(entry, "distanceDownhill").Value);
			float distanceUphill = float.Parse(GetAttribute(entry, "distanceUphill").Value);
			float heartrate = float.Parse(GetAttribute(entry, "heartrate").Value);
			float incline = float.Parse(GetAttribute(entry, "incline").Value);
			float latitude = float.Parse(GetAttribute(entry, "latitude").Value);
			float longitude = float.Parse(GetAttribute(entry, "longitude").Value);
			float power = float.Parse(GetAttribute(entry, "power").Value);
			float powerPerKG = float.Parse(GetAttribute(entry, "powerPerKG").Value);
			float riseRate = float.Parse(GetAttribute(entry, "riseRate").Value);
			float speed = float.Parse(GetAttribute(entry, "speed").Value);
			float temperature = float.Parse(GetAttribute(entry, "temperature").Value);
			int trainingTime = int.Parse(GetAttribute(entry, "trainingTime").Value);
			int trainingTimeAbsolute = int.Parse(GetAttribute(entry, "trainingTimeAbsolute").Value);
			int trainingTimeDownhill = int.Parse(GetAttribute(entry, "trainingTimeDownhill").Value);
			int trainingTimeUphill = int.Parse(GetAttribute(entry, "trainingTimeUphill").Value);
			float workInKJ = float.Parse(GetAttribute(entry, "workInKJ").Value);
			bool isActive = TryGetAttribute(entry, "isActive")?.Value == "1";
			int timeBelowIntensityZones = int.Parse(GetAttribute(entry, "timeBelowIntensityZones").Value);
			int timeInIntensityZone1 = int.Parse(GetAttribute(entry, "timeInIntensityZone1").Value);
			int timeInIntensityZone2 = int.Parse(GetAttribute(entry, "timeInIntensityZone2").Value);
			int timeInIntensityZone3 = int.Parse(GetAttribute(entry, "timeInIntensityZone3").Value);
			int timeInIntensityZone4 = int.Parse(GetAttribute(entry, "timeInIntensityZone4").Value);
			int timeAboveIntensityZones = int.Parse(GetAttribute(entry, "timeAboveIntensityZones").Value);
			float normalizedPower = float.Parse(GetAttribute(entry, "normalizedPower").Value);
			float rightBalance = float.Parse(GetAttribute(entry, "rightBalance").Value);
			float leftBalance = float.Parse(GetAttribute(entry, "leftBalance").Value);
			int timeInPowerZone1 = int.Parse(GetAttribute(entry, "timeInPowerZone1").Value);
			int timeInPowerZone2 = int.Parse(GetAttribute(entry, "timeInPowerZone2").Value);
			int timeInPowerZone3 = int.Parse(GetAttribute(entry, "timeInPowerZone3").Value);
			int timeInPowerZone4 = int.Parse(GetAttribute(entry, "timeInPowerZone4").Value);
			int timeInPowerZone5 = int.Parse(GetAttribute(entry, "timeInPowerZone5").Value);
			int timeInPowerZone6 = int.Parse(GetAttribute(entry, "timeInPowerZone6").Value);
			int timeInPowerZone7 = int.Parse(GetAttribute(entry, "timeInPowerZone7").Value);
			int pedalingTime = int.Parse(GetAttribute(entry, "pedalingTime").Value);
			bool useForChart = GetAttribute(entry, "useForChart").Value != "0";
			bool useForTrack = GetAttribute(entry, "useForTrack").Value != "0";
			int speedTime = int.Parse(GetAttribute(entry, "speedTime").Value);

			return new Entry(
				altitude,
				altitudeDifferencesDownhill,
				altitudeDifferencesUphill,
				cadence,
				calories,
				distance,
				distanceAbsolute,
				distanceDownhill,
				distanceUphill,
				heartrate,
				incline,
				latitude,
				longitude,
				power,
				powerPerKG,
				riseRate,
				speed,
				temperature,
				trainingTime,
				trainingTimeAbsolute,
				trainingTimeDownhill,
				trainingTimeUphill,
				workInKJ,
				isActive,
				timeBelowIntensityZones,
				timeInIntensityZone1,
				timeInIntensityZone2,
				timeInIntensityZone3,
				timeInIntensityZone4,
				timeAboveIntensityZones,
				normalizedPower,
				rightBalance,
				leftBalance,
				timeInPowerZone1,
				timeInPowerZone2,
				timeInPowerZone3,
				timeInPowerZone4,
				timeInPowerZone5,
				timeInPowerZone6,
				timeInPowerZone7,
				pedalingTime,
				useForChart,
				useForTrack,
				speedTime);
		}
		private static Marker XmlToMarker(XElement marker)
		{
			int altitudeDownhill = int.Parse(TryGetAttribute(marker, "altitudeDownhill")?.Value ?? "0");
			int altitudeUphill = int.Parse(TryGetAttribute(marker, "altitudeUphill")?.Value ?? "0");
			int averageAltitude = int.Parse(TryGetAttribute(marker, "averageAltitude")?.Value ?? "0");
			byte averageCadence = byte.Parse(TryGetAttribute(marker, "averageCadence")?.Value ?? "0");
			byte averageHeartrate = byte.Parse(TryGetAttribute(marker, "averageHeartrate")?.Value ?? "0");
			float averageInclineDownhill = float.Parse(TryGetAttribute(marker, "averageInclineDownhill")?.Value ?? "0");
			float averageInclineUphill = float.Parse(TryGetAttribute(marker, "averageInclineUphill")?.Value ?? "0");
			ushort averagePower = ushort.Parse(TryGetAttribute(marker, "averagePower")?.Value ?? "0");
			float averageSpeed = float.Parse(TryGetAttribute(marker, "averageSpeed")?.Value ?? "0");
			ushort calories = ushort.Parse(TryGetAttribute(marker, "calories")?.Value ?? "0");
			string description = GetAttribute(marker, "description").Value;
			float distance = float.Parse(GetAttribute(marker, "distance").Value);
			float distanceAbsolute = float.Parse(GetAttribute(marker, "distanceAbsolute").Value);
			int duration = int.Parse(GetAttribute(marker, "duration").Value);
			bool fastLap = bool.Parse(TryGetAttribute(marker, "fastLap")?.Value ?? "false");
			float latitude = float.Parse(GetAttribute(marker, "latitude").Value);
			float longitude = float.Parse(GetAttribute(marker, "longitude").Value);
			int maximumAltitude = int.Parse(TryGetAttribute(marker, "maximumAltitude")?.Value ?? "0");
			byte maximumCadence = byte.Parse(TryGetAttribute(marker, "maximumCadence")?.Value ?? "0");
			byte maximumHeartrate = byte.Parse(TryGetAttribute(marker, "maximumHeartrate")?.Value ?? "0");
			float maximumInclineDownhill = float.Parse(TryGetAttribute(marker, "maximumInclineDownhill")?.Value ?? "0");
			float maximumInclineUphill = float.Parse(TryGetAttribute(marker, "maximumInclineUphill")?.Value ?? "0");
			ushort maximumPower = ushort.Parse(TryGetAttribute(marker, "maximumPower")?.Value ?? "0");
			float maximumSpeed = float.Parse(TryGetAttribute(marker, "maximumSpeed")?.Value ?? "0");
			byte minimumHeartrate = byte.Parse(TryGetAttribute(marker, "minimumHeartrate")?.Value ?? "0");
			float minimumSpeed = float.Parse(TryGetAttribute(marker, "minimumSpeed")?.Value ?? "0");
			int number = int.Parse(GetAttribute(marker, "number").Value);
			int time = int.Parse(GetAttribute(marker, "time").Value);
			int timeAbsolute = int.Parse(GetAttribute(marker, "timeAbsolute").Value);
			string title = GetAttribute(marker, "title").Value;
			string type = GetAttribute(marker, "type").Value;
			ushort normalizedPower = ushort.Parse(TryGetAttribute(marker, "normalizedPower")?.Value ?? "0");
			float averageBalance = float.Parse(TryGetAttribute(marker, "averageBalance")?.Value ?? "0");
			int pedalingTime = int.Parse(TryGetAttribute(marker, "pedalingTime")?.Value ?? "0");
			float leftTorqueEffectivity = float.Parse(TryGetAttribute(marker, "leftTorqueEffectivity")?.Value ?? "0");
			float rightTorqueEffectivity = float.Parse(TryGetAttribute(marker, "rightTorqueEffectivity")?.Value ?? "0");
			float leftPedalingSmoothness = float.Parse(TryGetAttribute(marker, "leftPedalingSmoothness")?.Value ?? "0");
			float rightPedalingSmoothness = float.Parse(TryGetAttribute(marker, "rightPedalingSmoothness")?.Value ?? "0");
			if(type == "l")
			{
				return new Lap(
					altitudeDownhill,
					altitudeUphill,
					averageAltitude,
					averageCadence,
					averageHeartrate,
					averageInclineDownhill,
					averageInclineUphill,
					averagePower,
					averageSpeed,
					calories,
					description,
					distance,
					distanceAbsolute,
					duration,
					fastLap,
					latitude,
					longitude,
					maximumAltitude,
					maximumCadence,
					maximumHeartrate,
					maximumInclineDownhill,
					maximumInclineUphill,
					maximumPower,
					maximumSpeed,
					minimumHeartrate,
					minimumSpeed,
					number,
					time,
					timeAbsolute,
					title,
					type,
					normalizedPower,
					averageBalance,
					pedalingTime,
					leftTorqueEffectivity,
					rightTorqueEffectivity,
					leftPedalingSmoothness,
					rightPedalingSmoothness
				);
			}
			else if( type == "p")
			{
				return new Pause(
					altitudeDownhill,
					altitudeUphill,
					averageAltitude,
					averageCadence,
					averageHeartrate,
					averageInclineDownhill,
					averageInclineUphill,
					averagePower,
					averageSpeed,
					calories,
					description,
					distance,
					distanceAbsolute,
					duration,
					fastLap,
					latitude,
					longitude,
					maximumAltitude,
					maximumCadence,
					maximumHeartrate,
					maximumInclineDownhill,
					maximumInclineUphill,
					maximumPower,
					maximumSpeed,
					minimumHeartrate,
					minimumSpeed,
					number,
					time,
					timeAbsolute,
					title,
					type,
					normalizedPower,
					averageBalance,
					pedalingTime,
					leftTorqueEffectivity,
					rightTorqueEffectivity,
					leftPedalingSmoothness,
					rightPedalingSmoothness
				);
			}
			else
			{
				throw new NotImplementedException("Marker type is not supported");
			}
		}

		private static User XmlToUser(XElement user)
		{
			int color = int.Parse(GetAttribute(user, "color").Value);
			string gender = GetAttribute(user, "gender").Value;
			string type = user.Value;
			return new User(color, gender, type);
		}

		private static XElement GetRootElement(XDocument document)
		{
			XElement? root = document.Root;
			return root ?? throw new InvalidOperationException("The Root element is not defined");
		}
		private static XElement GetElement(XElement element, string name)
		{
			XElement? newElement = element.Element(name);
			return newElement ?? throw new InvalidOperationException($"The '{name}' element is not defined");
		}
		
		private static XElement? TryGetElement(XElement element, string name)
		{
			return element.Element(name);
		}

		private static XAttribute GetAttribute(XElement element, string name)
		{
			XAttribute? attribute = element.Attribute(name);
			return attribute ?? throw new InvalidOperationException($"The '{name}' attribute is not defined");
		}

		private static XAttribute? TryGetAttribute(XElement element, string name)
		{
			return element.Attribute(name);
		}
	}
}
