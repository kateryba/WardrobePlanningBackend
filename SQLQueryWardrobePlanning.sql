use WardrobePlanning;

insert into Cloth(Cloth.Size, Cloth.[Type], Cloth.[Owner], Color,Cloth.Season, Inserted, LastUpdated) 
output INSERTED.ID values (10, (select ID from ClothType where ClothType.[Name] = 'Top'), 
(select ID from FamilyMember where FamilyMember.[Name] = 'Tim'), 
'blue', 
(select ID from ClothSeason where ClothSeason.[Name] = 'winter'), 
GETUTCDATE(), 
GETUTCDATE());