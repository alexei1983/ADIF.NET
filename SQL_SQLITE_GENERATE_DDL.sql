SELECT
  Name || ' ' ||
  CASE  WHEN Name = 'PUBLIC_KEY' THEN 'varchar(8000)'
		WHEN DataTypeIndicator = 'S' OR DataTypeIndicator = 'I' THEN 'nvarchar(500)'
		WHEN DataTypeIndicator = 'G' OR DataTypeIndicator = 'M' THEN 'nvarchar(2000)'
		WHEN DataTypeIndicator = 'D' OR DataTypeIndicator = 'T' THEN 'datetime'
		WHEN DataTypeIndicator = 'N' THEN 'float'
		WHEN DataTypeIndicator = 'C' OR DataTypeIndicator = 'P' THEN 'nvarchar(2000)'
		WHEN DataTypeIndicator = 'L' OR DataTypeIndicator = 'E' THEN 'varchar(255)'
		WHEN DataTypeIndicator = 'B' THEN 'bit'
  END || ','
 FROM Tags WHERE IsHeader = 0
 ORDER BY Name