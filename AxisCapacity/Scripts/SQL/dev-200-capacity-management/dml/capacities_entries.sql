BEGIN
   IF NOT EXISTS (select 1 from dbo.Terminals where name = 'DAGENHAM')
   BEGIN
       insert into dbo.Terminals(name) values ('DAGENHAM');
   END
END

INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Dagenham', 'AM', 34498, 1.62, 7, 7, 7, 7, 7, 5, 5);
INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Dagenham', 'PM', 34498, 1.61, 3, 3, 3, 3, 3, 2, 3);

INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Grays', 'AM', 34498, 1.62, 7, 7, 7, 7, 7, 4, 3);
INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Grays', 'PM', 34498, 1.61, 3, 3, 3, 3, 3, 4, 1);

INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Westerleigh', 'AM', 34096, 2.34, 1, 1, 1, 1, 1, 0, 0);
INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Westerleigh', 'PM', 34096, 2.32, 1, 1, 1, 1, 1, 0, 0);

INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Kingsbury', 'AM', 33862, 2.34, 2, 2, 2, 2, 2, 2, 1);
INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Kingsbury', 'PM', 33862, 2.39, 1, 1, 1, 1, 1, 0, 0);

INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Immingham', 'AM', 34043, 1.66, 4, 4, 4, 4, 4, 1, 1);
INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Immingham', 'PM', 34043, 1.65, 3, 3, 3, 3, 3, 0, 0);

INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Jarrow', 'AM', 33876, 1.72, 1, 1, 1, 1, 1, 0, 0);
INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Jarrow', 'PM', 33876, 1.83, 1, 1, 1, 1, 1, 0, 0);

INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Grangemouth', 'AM', 33013, 1.88, 2, 2, 2, 2, 2, 1, 1);
INSERT INTO dbo.Capacity(terminal, shift, avg_load, avg_deliveries, mon, tue, wed, thu, fri, sat, sun) VALUES ('Grangemouth', 'PM', 33013, 1.95, 3, 3, 3, 3, 3, 0, 1);


insert into dbo.TerminalGrouping(terminal, group_id) values ('Dagenham', 1);
insert into dbo.TerminalGrouping(terminal, group_id) values ('Grays', 1);