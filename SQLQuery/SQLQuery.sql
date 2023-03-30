DROP TABLE [Lab2_1].[dbo].[temporary]
DROP TABLE [Lab2_1].[dbo].[stop_on_the_road]
DROP TABLE [Lab2_1].[dbo].[road]
DROP TABLE [Lab2_1].[dbo].[placement_along_the_road]
DROP TABLE [Lab2_1].[dbo].[locality_name]
GO
CREATE TABLE [Lab2_1].[dbo].[temporary]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [road_name] nvarchar(50) NOT NULL, 
    [length] FLOAT NOT NULL, 
    [bus_stop_name] nvarchar(50) NOT NULL, 
    [bus_position] nvarchar(10) NOT NULL, 
    [is_have_pavilion] nvarchar(10) NOT NULL
)
GO

-- truncate the table first
TRUNCATE TABLE [Lab2_1].[dbo].[temporary];
GO

-- import the file
BULK INSERT [Lab2_1].[dbo].[temporary]
FROM 'D:\station.csv'
WITH
(
		CODEPAGE = '1251',
        FORMAT='CSV',
		FIELDTERMINATOR = ';',
		ROWTERMINATOR='\n',
        FIRSTROW=2,
		TABLOCK
)
GO

CREATE TABLE [Lab2_1].[dbo].[placement_along_the_road]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[placement_along_the_road] nvarchar(50) NOT NULL
)

CREATE TABLE [Lab2_1].[dbo].[locality_name]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[locality_name] NVARCHAR(50) NOT NULL UNIQUE
)
GO

CREATE TABLE [Lab2_1].[dbo].[road]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[start_point_id] INT NOT NULL,
	[end_point_id] INT NOT NULL,
	CONSTRAINT [FK_road_sp_to_locality_name] FOREIGN KEY ([start_point_id]) REFERENCES [Lab2_1].[dbo].[locality_name]([id]),
	CONSTRAINT [FK_road_ep_to_locality_name] FOREIGN KEY ([end_point_id]) REFERENCES [Lab2_1].[dbo].[locality_name]([id])
)
GO

CREATE TABLE [Lab2_1].[dbo].[stop_on_the_road]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY,
	[road_id] INT NOT NULL,
	[is_have_pavilion] nvarchar(50) NOT NULL,
	[placement_along_the_road_id] INT NOT NULL,
	[range_from_start] FLOAT NOT NULL,
	[bus_stop_name] NVARCHAR(50) NOT NULL,
	CONSTRAINT [FK_stop_ont_the_road_to_road] FOREIGN KEY ([road_id]) REFERENCES [Lab2_1].[dbo].[road]([id]),
	CONSTRAINT [FK_stop_ont_the_road_to_placement_along_the_road] FOREIGN KEY ([placement_along_the_road_id]) REFERENCES [Lab2_1].[dbo].[placement_along_the_road]([id]),
)
GO

INSERT INTO [Lab2_1].[dbo].[placement_along_the_road](placement_along_the_road)
SELECT bus_position 
FROM [dbo].[temporary]
GROUP BY bus_position
GO

WITH first_city AS 
(
	SELECT TRIM(SUBSTRING(road_name, 0, CHARINDEX('-', road_name))) AS [City]
	FROM [Lab2_1].[dbo].[temporary]
), last_city AS
(
	SELECT TRIM(REVERSE(SUBSTRING(REVERSE(road_name), 0, CHARINDEX('-', REVERSE(road_name))))) AS [City]
	FROM [Lab2_1].[dbo].[temporary]
)
INSERT INTO [Lab2_1].[dbo].[locality_name]([locality_name])
SELECT [City]
FROM first_city
WHERE [City] != ''
UNION
SELECT [City] 
FROM last_city
WHERE [City] != ''
GO

INSERT INTO [LAB2_1].[dbo].[road]([start_point_id], [end_point_id])
SELECT DISTINCT lnm_sp.id, lnm_fp.id
FROM [Lab2_1].[dbo].[temporary] AS sd
    INNER JOIN [Lab2_1].[dbo].[locality_name] AS lnm_sp ON lnm_sp.locality_name = TRIM(SUBSTRING(sd.road_name, 0, CHARINDEX('-', sd.road_name)))
    INNER JOIN [Lab2_1].[dbo].[locality_name] AS lnm_fp ON lnm_fp.locality_name = TRIM(REVERSE(SUBSTRING(REVERSE(sd.road_name), 0, CHARINDEX('-', REVERSE(sd.road_name)))));
GO

INSERT INTO [Lab2_1].[dbo].[stop_on_the_road](is_have_pavilion, bus_stop_name, range_from_start, placement_along_the_road_id, road_id)
SELECT DISTINCT 
		CASE WHEN sd.is_have_pavilion = '' THEN 'Не указано' ELSE sd.is_have_pavilion END, 
		CASE WHEN sd.bus_stop_name = '' THEN 'Не указано' ELSE sd.bus_stop_name END, 
		sd.length, 
		pl_r.id, 
		r.id
FROM [Lab2_1].[dbo].[temporary] AS sd
	INNER JOIN [Lab2_1].[dbo].[placement_along_the_road] AS pl_r ON sd.bus_position = pl_r.placement_along_the_road
	INNER JOIN [Lab2_1].[dbo].[locality_name] AS lnm_sp ON lnm_sp.locality_name = TRIM(SUBSTRING(sd.road_name, 0, CHARINDEX('-', sd.road_name))) 
    INNER JOIN [Lab2_1].[dbo].[locality_name] AS lnm_fp ON lnm_fp.locality_name = TRIM(REVERSE(SUBSTRING(REVERSE(sd.road_name), 0, CHARINDEX('-', REVERSE(sd.road_name)))))
	INNER JOIN [Lab2_1].[dbo].[road] AS r ON r.start_point_id = lnm_sp.id AND r.end_point_id = lnm_fp.id;
GO

-- search data scripts
-- Выбрать все остановки с павильоном
SELECT 
	ln_sp.locality_name AS start_point, 
	ln_fp.locality_name AS finish_point,
	st_r.range_from_start,
	st_r.bus_stop_name,
	pl_r.placement_along_the_road,
	st_r.is_have_pavilion
FROM stop_on_the_road AS st_r
	INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
	INNER JOIN road AS r ON st_r.road_id = r.id
	INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point_id
	INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.end_point_id
WHERE
	st_r.is_have_pavilion = 'Есть';	
GO

-- Выбрать все остановки слева от дороги “Звенигово - Шелангер - Морки”
SELECT 
	ln_sp.locality_name AS start_point, 
	ln_fp.locality_name AS finish_point,
	st_r.range_from_start,
	st_r.bus_stop_name,
	pl_r.placement_along_the_road,
	st_r.is_have_pavilion
FROM stop_on_the_road AS st_r
	INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
	INNER JOIN road AS r ON st_r.road_id = r.id
	INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point_id
	INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.end_point_id
WHERE
	ln_sp.locality_name = 'Звенигово' AND ln_fp.locality_name = 'Морки'
	AND pl_r.placement_along_the_road = 'Слева';

-- Выбрать все остановки по названию “Дачи”
SELECT 
	ln_sp.locality_name AS start_point, 
	ln_fp.locality_name AS finish_point,
	st_r.range_from_start,
	st_r.bus_stop_name,
	pl_r.placement_along_the_road,
	st_r.is_have_pavilion
FROM stop_on_the_road AS st_r
	INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
	INNER JOIN road AS r ON st_r.road_id = r.id
	INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point_id
	INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.end_point_id
WHERE
	st_r.bus_stop_name = 'Дачи';

--Выбрать все остановки в интервале от 20 до 80 километров включительно на дороге “Йошкар-Ола - Уржум”
SELECT 
	ln_sp.locality_name AS start_point, 
	ln_fp.locality_name AS finish_point,
	st_r.range_from_start,
	st_r.bus_stop_name,
	pl_r.placement_along_the_road,
	st_r.is_have_pavilion
FROM stop_on_the_road AS st_r
	INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
	INNER JOIN road AS r ON st_r.road_id = r.id
	INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point_id
	INNER JOIN locality_name AS ln_fp ON ln_fp.id = r.end_point_id
WHERE
	ln_sp.locality_name = 'Йошкар–Ола' AND ln_fp.locality_name = 'Уржум' AND
	st_r.range_from_start > 20.0 AND st_r.range_from_start <= 80.0;

--найти минимальное расстояние между остановками на различных автомобильных дорогах с условием, 
--что перемещение между дорогами происходит в конечных пунктах (например, в населённых пунктах “Звенигово” и “Морки” на 
--дороге “Звенигово - Шелангер - Морки”).
WITH first_road AS 
(
	SELECT 
		ln_sp.locality_name AS start_point, 
		ln_ep.locality_name AS end_point,
		st_r.bus_stop_name,
		st_r.range_from_start
	FROM stop_on_the_road AS st_r
		INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
		INNER JOIN road AS r ON st_r.road_id = r.id
		INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point_id
		INNER JOIN locality_name AS ln_ep ON ln_ep.id = r.end_point_id	
), second_road AS 
(
	SELECT 
		ln_sp.locality_name AS start_point, 
		ln_ep.locality_name AS end_point,
		st_r.bus_stop_name,
		st_r.range_from_start
	FROM stop_on_the_road AS st_r
		INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
		INNER JOIN road AS r ON st_r.road_id = r.id
		INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point_id
		INNER JOIN locality_name AS ln_ep ON ln_ep.id = r.end_point_id
),
max_range AS (
	SELECT DISTINCT		
		ln_sp.locality_name AS start_point, 
		ln_ep.locality_name AS end_point,
		MAX(st_r.range_from_start) AS max_range
	FROM stop_on_the_road AS st_r
		INNER JOIN placement_along_the_road AS pl_r ON st_r.placement_along_the_road_id = pl_r.id
		INNER JOIN road AS r ON st_r.road_id = r.id
		INNER JOIN locality_name AS ln_sp ON ln_sp.id = r.start_point_id
		INNER JOIN locality_name AS ln_ep ON ln_ep.id = r.end_point_id
	GROUP BY ln_sp.locality_name, ln_ep.locality_name	
)
SELECT
	fr.bus_stop_name,
	sr.bus_stop_name,
	round(mr.max_range - fr.range_from_start + sr.range_from_start, 3) AS range
FROM first_road AS fr
	INNER JOIN second_road AS sr ON fr.end_point = sr.start_point
	INNER JOIN max_range AS mr ON fr.start_point = mr.start_point AND fr.end_point = mr.end_point;
	