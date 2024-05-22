          
 Declare @id int              
 Declare @employeeID varchar(10)                         
 Declare @expanderNum varchar(10)                  
 Declare @length numeric(18,5)               
 Declare @width numeric(18,5)              
 Declare @weight numeric(18,5)              
 Declare @comments varchar(80)              
 Declare @densityPCF numeric(18,5)              
 Declare @densityPSF numeric(18,5)            
 Declare @QrCodeDate date            
 Declare @CellCount int          
 Declare @workOrder varchar(30)          
 Declare @block_batch varchar(10)             
 Declare @sliceNum int          
 Declare @saw varchar(10)          
 Declare @max numeric(18,5)           
 Declare @min numeric(18,5)           
 Declare @ave numeric(18,5)      
 Declare @badge varchar(10)      
 


SET  @id = -1
SET @employeeID = N'0096'
SET @expanderNum = N'1'
SET @length = 152
SET @width = 43
SET @weight = 6.3
SET @comments = NULL
SET @densityPCF = 3.4845311195669431
SET @densityPSF = 0.13880048959608324
SET @QrCodeDate = getdate()--'21-05-2024 12:00:00 AM'
SET @CellCount = 10
SET @workOrder = N'1594221'
SET @block_batch = N'0000177801'
SET @sliceNum = 2
SET @saw = '10A'
SET @max = 0.381
SET @min = 0.378
SET @ave = 0.379
SET @badge = N'sgsg'
         
        
    
    
if ( @id < 0)    
Begin    
if Exists ( select 1 from [dbo].[SliceData] where WorkOrder = @workOrder     
       and BlockNumber = @block_batch     
       and SliceNum = @sliceNum    
       and SawNum = @saw    
       and MinThk = @min    
       and MaxThk = @max    
       and AvgThk = @ave )   
      -- and Badge = @badge)    
   Begin    
    select @id = Id from [dbo].[SliceData] where WorkOrder = @workOrder     
           and BlockNumber = @block_batch     
           and SliceNum = @sliceNum    
           and SawNum = @saw    
           and MinThk = @min    
           and MaxThk = @max    
           and AvgThk = @ave    
           --and Badge = @badge    
     
   END    
END    
    
Declare @sliceID int = @id       
          
If Exists (select 1 from [dbo].[SliceData] where id = @id)          
 BEGIN          
  UPDATE [dbo].[SliceData]              
     SET [EmployeeID] = @employeeID              
     ,[ExpanderNum] = @expanderNum              
     ,[Length] = @length              
     ,[Width] = @width              
     ,[Weight] = @weight              
     ,[Comments] = @comments              
     ,[DensityPCF] = @densityPCF              
     ,[DensityPSF] = @densityPSF              
     ,[QRCodeDate]=@QrCodeDate            
     ,[CellCount] = @CellCount   
  ,[badge] = @badge  
   WHERE ID = @id            
 END          
 ELSE          
 BEGIN          
 Declare  @thickness numeric(18,5)          
 select @thickness= [Thickness] from [dbo].[ItemMaster] im          
 inner join WorkOrderHeader wo on wo.Material = im.[Material]          
 where wo.workOrder = @workOrder          
  INSERT INTO [dbo].[SliceData]          
       ( [WorkOrder]          
     ,[BlockNumber]          
     ,[SliceNum]          
     ,[EmployeeID]          
     ,[SawNum]          
     ,[ExpanderNum]          
     ,[Length]          
     ,[Width]          
     ,[Weight]          
     ,[MinThk]          
     ,[MaxThk]          
     ,[AvgThk]          
     ,[Comments]          
     ,[DensityPCF]          
     ,[DensityPSF]          
     ,[CellCount]          
     ,[QRCodeDate]          
     ,[Thickness]      
     ,[badge]      
       )          
    VALUES          
       (@workOrder          
       ,@block_batch          
       ,@sliceNum          
       ,@employeeID          
       ,@saw          
       ,@expanderNum          
       ,@length          
       ,@width          
       ,@weight          
       ,@min          
       ,@max          
       ,@ave          
       ,@comments          
       ,@densityPCF          
       ,@densityPSF          
       ,@CellCount          
       ,@QrCodeDate          
       ,@thickness      
    , @badge)          
     select @sliceID = @@IDENTITY        
 END    
   
 Declare @T_Thickness numeric(18,5)    
 Declare @T_ThicknessTol numeric(18,5)    
 Declare @T_Density numeric(18,5)    
 Declare @T_DensityTol numeric(18,5)    
 Declare @T_FailCode varchar(10) = Null  
  
 SELECT   
  @T_Thickness= IM.Thickness,   
  @T_ThicknessTol= IM.ThicknessTol,   
  @T_Density= IM.Density,   
  @T_DensityTol = IM.DensityTol  
 FROM ItemMaster IM  
 Inner Join WorkOrderHeader WOH on WOH.Material = IM.Material  
 Inner Join SliceData SD on SD.WorkOrder = WOH.WorkOrder  
  WHERE SD.Id = @sliceID   
  
  If ( @ave > (@T_Thickness + @T_ThicknessTol) )  
  Begin  
 SET @T_FailCode ='TH'  
  END  
  
  If ( @ave < (@T_Thickness - @T_ThicknessTol) )  
  Begin  
 SET @T_FailCode ='TL'  
  END  
  
    If ( @densityPCF > (@T_Density + @T_DensityTol) )  
  Begin  
 SET @T_FailCode ='DH'  
  END  
  
    If ( @densityPCF < (@T_Density - @T_DensityTol) )  
  Begin  
 SET @T_FailCode ='DL'  
  END  
  
   
  UPDATE [dbo].[SliceData]              
     SET [FailCode] = @T_FailCode     
   WHERE ID = @sliceID        
        
 select @sliceID     
 
 --delete  from slicedata where id = 10304

 --select * from SliceData where ID = 10304