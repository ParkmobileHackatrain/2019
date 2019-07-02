SELECT * FROM robotaxi.AvailableParking;





select Latitude, Longitude, ObservationTime, 1 from AvailableParking 
union select Latitude, Longitude, StopTime as ObservationTime, 2 from ParkingHistory where StopTime < '2019-05-18 00:50:00' and StopTime > addtime('2019-05-18 00:50:00', '-00:05')
union select * from export_bmw_tiny where ObservationTime < '2019-05-18 10:50:00' and ObservationTime > addtime('2019-05-18 10:50:00', '-00:05');


0	98	03:45:40	select * from AvailableParking union
 select Latitude, Longitude, StopTime as ObservationTime, 2 from ParkingHistory where StopTime < '2019-05-18 00:50:00' and StopTime > addtime('2019-05-18 00:50:00', '-00:05')	Error Code: 1222. The used SELECT statements have a different number of columns	0.032 sec