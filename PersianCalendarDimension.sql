
drop function dbo.G2J
drop table DateDimension
drop table #dim



/*
CopyRight
http://rastan.parsiblog.com/Posts/381/%D8%AA%D8%A8%D8%AF%D9%8A%D9%84+%D8%B3%D8%A7%D8%AF%D9%87+%D8%AA%D8%A7%D8%B1%D9%8A%D8%AE+%D9%85%D9%8A%D9%84%D8%A7%D8%AF%D9%8A+%D8%A8%D9%87+%D8%B4%D9%85%D8%B3%D9%8A+%D8%AF%D8%B1+SQL+Server/
*/


EXEC('

CREATE FUNCTION dbo.[G2J] ( @intDate DATETIME, @Format NVARCHAR(max)) 
RETURNS NVARCHAR(max)
BEGIN
/* Format Rules: (پنجشنبه 7 اردیبهشت 1394)
ChandShanbe -> پنجشنبه (روز هفته به حروف)
ChandShanbeAdadi -> 6 (روز هفته به عدد)
Rooz -> 7 (چندمین روز از ماه)
Rooz2 -> 07 (چندمین روز از ماه دو کاراکتری)
Maah -> 2 (چندمین ماه از سال)
Maah2 -> 02 (چندمین ماه از سال دو کاراکتری)
MaahHarfi -> اردیبهشت (نام ماه به حروف)
Saal -> 1394 (سال چهار کاراکتری)
Saal2 -> 94 (سال دو کاراکتری)
Saal4 -> 1394 (سال چهار کاراکتری)
SaalRooz -> 38 (چندمین روز سال)
Default Format -> ''ChandShanbe Rooz MaahHarfi Saal''
*/
DECLARE @shYear AS INT ,@shMonth AS INT ,@shDay AS INT ,@intYY AS INT ,@intMM AS INT ,@intDD AS INT ,@Kabiseh1 AS INT ,@Kabiseh2 AS INT ,@d1 AS INT ,@m1 AS INT, @shMaah AS NVARCHAR(max),@shRooz AS NVARCHAR(max),@DayCnt AS INT, @YearDay AS INT
DECLARE @DayDate AS NVARCHAR(max)

SET @intYY = DATEPART(yyyy, @intDate)

IF @intYY < 1000 SET @intYY = @intYY + 2000

SET @intMM = MONTH(@intDate)
SET @intDD = DAY(@intDate)
SET @shYear = @intYY - 622
IF (@Format IS NULL) OR NOT LEN(@Format)>0 SET @Format = ''ChandShanbe Rooz MaahHarfi Saal''

SET @m1 = 1
SET @d1 = 1
SET @shMonth = 10
SET @shDay = 11
SET @DayCnt = datepart(dw, ''01/02/'' + CONVERT(CHAR(4), @intYY))
SET @YearDay = 276

IF ( ( @intYY - 1993 ) % 4 = 0 ) SET @shDay = 12
SET @YearDay = @YearDay + @shDay

WHILE ( @m1 != @intMM ) OR ( @d1 != @intDD )
BEGIN

 SET @d1 = @d1 + 1
 SET @DayCnt = @DayCnt + 1

 IF ( ( @intYY - 1992 ) % 4 = 0) SET @Kabiseh1 = 1 ELSE SET @Kabiseh1 = 0

 IF ( ( @shYear - 1371 ) % 4 = 0) SET @Kabiseh2 = 1 ELSE SET @Kabiseh2 = 0

 IF 
 (@d1 = 32 AND (@m1 = 1 OR @m1 = 3 OR @m1 = 5 OR @m1 = 7 OR @m1 = 8 OR @m1 = 10 OR @m1 = 12))
 OR
 (@d1 = 31 AND (@m1 = 4 OR @m1 = 6 OR @m1 = 9 OR @m1 = 11))
 OR
 (@d1 = 30 AND @m1 = 2 AND @Kabiseh1 = 1)
 OR
 (@d1 = 29 AND @m1 = 2 AND @Kabiseh1 = 0)
 BEGIN
  SET @m1 = @m1 + 1
  SET @d1 = 1
 END

 IF @m1 > 12
 BEGIN
  SET @intYY = @intYY + 1
  SET @m1 = 1
 END
 
 IF @DayCnt > 7 SET @DayCnt = 1

SET @shDay = @shDay + 1
SET @YearDay = @YearDay + 1
 
 IF
 (@shDay = 32 AND @shMonth < 7)
 OR
 (@shDay = 31 AND @shMonth > 6 AND @shMonth < 12)
 OR
 (@shDay = 31 AND @shMonth = 12 AND @Kabiseh2 = 1)
 OR
 (@shDay = 30 AND @shMonth = 12 AND @Kabiseh2 = 0)
 BEGIN
  SET @shMonth = @shMonth + 1
  SET @shDay = 1
 END

 IF @shMonth > 12
 BEGIN
  SET @shYear = @shYear + 1
  SET @shMonth = 1
  SET @YearDay = 1
 END
 
END

IF @shMonth=1 SET @shMaah=N''فروردین''
IF @shMonth=2 SET @shMaah=N''اردیبهشت''
IF @shMonth=3 SET @shMaah=N''خرداد''
IF @shMonth=4 SET @shMaah=N''تیر''
IF @shMonth=5 SET @shMaah=N''مرداد''
IF @shMonth=6 SET @shMaah=N''شهریور''
IF @shMonth=7 SET @shMaah=N''مهر''
IF @shMonth=8 SET @shMaah=N''آبان''
IF @shMonth=9 SET @shMaah=N''آذر''
IF @shMonth=10 SET @shMaah=N''دی''
IF @shMonth=11 SET @shMaah=N''بهمن''
IF @shMonth=12 SET @shMaah=N''اسفند''

IF @DayCnt=1 SET @shRooz=N''شنبه''
IF @DayCnt=2 SET @shRooz=N''یکشنبه''
IF @DayCnt=3 SET @shRooz=N''دوشنبه''
IF @DayCnt=4 SET @shRooz=N''سه شنبه''
IF @DayCnt=5 SET @shRooz=N''چهارشنبه''
IF @DayCnt=6 SET @shRooz=N''پنجشنبه''
IF @DayCnt=7 SET @shRooz=N''جمعه''

SET @DayDate = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@Format,''MaahHarfi'',@shMaah),''SaalRooz'',LTRIM(STR(@YearDay,3))),''ChandShanbeAdadi'',@DayCnt),''ChandShanbe'',@shRooz),''Rooz2'',REPLACE(STR(@shDay,2), '' '', ''0'')),''Maah2'',REPLACE(STR(@shMonth, 2), '' '', ''0'')),''Saal2'',SUBSTRING(STR(@shYear,4),3,2)),''Saal4'',STR(@shYear,4)),''Saal'',LTRIM(STR(@shYear,4))),''Maah'',LTRIM(STR(@shMonth,2))),''Rooz'',LTRIM(STR(@shDay,2)))
/* Format Samples:
Format=''ChandShanbe Rooz MaahHarfi Saal'' -> پنجشنبه 17 اردیبهشت 1394
Format=''Rooz MaahHarfi Saal'' -> ـ 17 اردیبهشت 1394
Format=''Rooz/Maah/Saal'' -> 1394/2/17
Format=''Rooz2/Maah2/Saal2'' -> 94/02/17
Format=''Rooz روز گذشته از MaahHarfi در سال Saal2'' -> ـ 17 روز گذشته از اردیبهشت در سال 94
*/
RETURN @DayDate
END

');


DECLARE @StartDate DATE = '20000101', @NumberOfYears INT = 30;
-- prevent set or regional settings from interfering with
-- interpretation of dates / literals
SET DATEFIRST 7;
SET DATEFORMAT mdy;
SET LANGUAGE US_ENGLISH;
DECLARE @CutoffDate DATE = DATEADD(YEAR, @NumberOfYears, @StartDate);
-- this is just a holding table for intermediate calculations:
CREATE TABLE #dim
(
[date] DATE PRIMARY KEY,
[day] AS DATEPART(DAY, [date]),
[month] AS DATEPART(MONTH, [date]),
FirstOfMonth AS CONVERT(DATE, DATEADD(MONTH, DATEDIFF(MONTH, 0, [date]), 0)),
[MonthName] AS DATENAME(MONTH, [date]),
[week] AS DATEPART(WEEK, [date]),
[ISOweek] AS DATEPART(ISO_WEEK, [date]),
[DayOfWeek] AS DATEPART(WEEKDAY, [date]),
[quarter] AS DATEPART(QUARTER, [date]),
[year] AS DATEPART(YEAR, [date]),
FirstOfYear AS CONVERT(DATE, DATEADD(YEAR, DATEDIFF(YEAR, 0, [date]), 0)),
Style112 AS CONVERT(CHAR(8), [date], 112),
Style101 AS CONVERT(CHAR(10), [date], 101)
);
-- use the catalog views to generate as many rows as we need
INSERT #dim([date])
SELECT d
FROM
(
SELECT d = DATEADD(DAY, rn - 1, @StartDate)
FROM
(
SELECT TOP (DATEDIFF(DAY, @StartDate, @CutoffDate))
rn = ROW_NUMBER() OVER (ORDER BY s1.[object_id])
FROM sys.all_objects AS s1
CROSS JOIN sys.all_objects AS s2
-- on my system this would support > 5 million days
ORDER BY s1.[object_id]
) AS x
) AS y;
CREATE TABLE dbo.DateDimension
(
DateKey INT NOT NULL PRIMARY KEY,
[Date] DATE NOT NULL,
[Day] TINYINT NOT NULL,
DaySuffix CHAR(2) NOT NULL,
[Weekday] TINYINT NOT NULL,
WeekDayName VARCHAR(10) NOT NULL,
IsWeekend BIT NOT NULL,
IsHoliday BIT NOT NULL,
HolidayText VARCHAR(64) SPARSE,
DOWInMonth TINYINT NOT NULL,
[DayOfYear] SMALLINT NOT NULL,
WeekOfMonth TINYINT NOT NULL,
WeekOfYear TINYINT NOT NULL,
ISOWeekOfYear TINYINT NOT NULL,
[Month] TINYINT NOT NULL,
[MonthName] VARCHAR(10) NOT NULL,
[Quarter] TINYINT NOT NULL,
QuarterName VARCHAR(6) NOT NULL,
[Year] INT NOT NULL,
MMYYYY CHAR(6) NOT NULL,
MonthYear CHAR(7) NOT NULL,
FirstDayOfMonth DATE NOT NULL,
LastDayOfMonth DATE NOT NULL,
FirstDayOfQuarter DATE NOT NULL,
LastDayOfQuarter DATE NOT NULL,
FirstDayOfYear DATE NOT NULL,
LastDayOfYear DATE NOT NULL,
FirstDayOfNextMonth DATE NOT NULL,
FirstDayOfNextYear DATE NOT NULL,
[PersianDate] nvarchar(50) NOT NULL
);
GO
INSERT dbo.DateDimension WITH (TABLOCKX)
SELECT
DateKey = CONVERT(INT, Style112),
[Date] = [date],
[Day] = CONVERT(TINYINT, [day]),
DaySuffix = CONVERT(CHAR(2), CASE WHEN [day] / 10 = 1 THEN 'th' ELSE
CASE RIGHT([day], 1) WHEN '1' THEN 'st' WHEN '2' THEN 'nd'
WHEN '3' THEN 'rd' ELSE 'th' END END),
[Weekday] = CONVERT(TINYINT, [DayOfWeek]),
[WeekDayName] = CONVERT(VARCHAR(10), DATENAME(WEEKDAY, [date])),
[IsWeekend] = CONVERT(BIT, CASE WHEN [DayOfWeek] IN (1,7) THEN 1 ELSE 0 END),
[IsHoliday] = CONVERT(BIT, 0),
HolidayText = CONVERT(VARCHAR(64), NULL),
[DOWInMonth] = CONVERT(TINYINT, ROW_NUMBER() OVER
(PARTITION BY FirstOfMonth, [DayOfWeek] ORDER BY [date])),
[DayOfYear] = CONVERT(SMALLINT, DATEPART(DAYOFYEAR, [date])),
WeekOfMonth = CONVERT(TINYINT, DENSE_RANK() OVER
(PARTITION BY [year], [month] ORDER BY [week])),
WeekOfYear = CONVERT(TINYINT, [week]),
ISOWeekOfYear = CONVERT(TINYINT, ISOWeek),
[Month] = CONVERT(TINYINT, [month]),
[MonthName] = CONVERT(VARCHAR(10), [MonthName]),
[Quarter] = CONVERT(TINYINT, [quarter]),
QuarterName = CONVERT(VARCHAR(6), CASE [quarter] WHEN 1 THEN 'First'
WHEN 2 THEN 'Second' WHEN 3 THEN 'Third' WHEN 4 THEN 'Fourth' END),
[Year] = [year],
MMYYYY = CONVERT(CHAR(6), LEFT(Style101, 2) + LEFT(Style112, 4)),
MonthYear = CONVERT(CHAR(7), LEFT([MonthName], 3) + LEFT(Style112, 4)),
FirstDayOfMonth = FirstOfMonth,
LastDayOfMonth = MAX([date]) OVER (PARTITION BY [year], [month]),
FirstDayOfQuarter = MIN([date]) OVER (PARTITION BY [year], [quarter]),
LastDayOfQuarter = MAX([date]) OVER (PARTITION BY [year], [quarter]),
FirstDayOfYear = FirstOfYear,
LastDayOfYear = MAX([date]) OVER (PARTITION BY [year]),
FirstDayOfNextMonth = DATEADD(MONTH, 1, FirstOfMonth),
FirstDayOfNextYear = DATEADD(YEAR, 1, FirstOfYear),
PersianDate = dbo.G2J([Date], 'Saal4/Maah/Rooz ChandShanbe')
FROM #dim
OPTION (MAXDOP 1);