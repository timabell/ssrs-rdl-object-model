USE [AdventureWorks]
GO

/****** Object:  StoredProcedure [dbo].[csp_Preview_Report]    Script Date: 01/12/2006 09:33:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLRDemo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CLRDemo] ;

IF EXISTS (SELECT [name] FROM sys.assemblies WHERE [name] = N'Reporting.CLR') 
DROP ASSEMBLY [Reporting.CLR];

IF EXISTS (SELECT [name] FROM sys.assemblies WHERE [name] = N'Reporting.ObjectModel.XmlSerializers') 
DROP ASSEMBLY [Reporting.ObjectModel.XmlSerializers];

IF EXISTS (SELECT [name] FROM sys.assemblies WHERE [name] = N'Reporting.ObjectModel') 
DROP ASSEMBLY [Reporting.ObjectModel];

-- Register System.Drawing
IF NOT EXISTS (SELECT [name] FROM sys.assemblies WHERE [name] = N'System.Drawing') 
CREATE ASSEMBLY [System.Drawing] 
from 'C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\System.Drawing.dll' with PERMISSION_SET = UNSAFE;

CREATE ASSEMBLY [Reporting.ObjectModel] 
from 'C:\Presentations\TechEd\2007\Source\bin\Reporting.ObjectModel.dll' with PERMISSION_SET = UNSAFE;

CREATE ASSEMBLY [Reporting.ObjectModel.XmlSerializers] 
from 'C:\Presentations\TechEd\2007\Source\bin\Reporting.ObjectModel.XmlSerializers.dll' with PERMISSION_SET = UNSAFE;
