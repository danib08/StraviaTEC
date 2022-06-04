exec post_athlete 'gabogh99', 'Gabriel', 'Gonzalez', 'foto.png', 23, '1999-03-12', 'siuuu', 'Costa Rica', 'Open'
exec post_athlete 'dani_08', 'Daniela', 'Brenes', 'foto.png', 21, '2001-05-08', 'dynamite', 'Costa Rica', 'Open'
exec post_athlete 'omend', 'Oscar', 'Mendez', 'foto.png', 21, '2000-3-29', 'cot', 'Costa Rica', 'Open'
exec post_athlete 'mjhca13', 'Maria Jesus', 'Hernandez', 'foto.png', 22, '1999-10-13', 'burbuja', 'Costa Rica', 'Open'



exec post_activity  'Act1','Nado en piscina','archivo.gpx','2022-05-17','09:29:02', 10.51, 'Nadar','gabogh99'
exec post_activity  'Act2','Pista atletismo','archivo.gpx','2022-05-17','02:12:42', 2.51, 'Correr','omend'
exec post_activity  'Act3','Subir el Chirripó','archivo.gpx','2022-05-17','20:15:11', 42.72, 'Caminata','dani_08'
exec post_activity 'Act4','Rafting por el Pacuare','archivo.gpx','2022-05-17','01:45:41', 7.51, 'Kayak','gabogh99'
exec post_activity 'Act5','Trote por la ciudad','archivo.gpx','2022-05-18','02:32:41', 8.91, 'Correr','omend'
exec post_activity 'Act7','Ciclismo montaña','archivo.gpx','2022-05-18','02:32:41', 8.91, 'Ciclismo','dani_08'

exec post_follower 'dani_08','gabogh99'
exec post_follower 'gabogh99','dani_08'
exec post_follower 'dani_08','omend'
exec post_follower 'omend','dani_08'
exec post_follower 'gabogh99','omend'
exec post_follower 'omend','gabogh99'

exec post_challenge 'Chal1','Reto fin de semana atleta','2022-05-18','2022-05-20','Privado', 23.29, 'Fondo'
exec post_challenge 'Chal2','Reto decatlón 2021','2022-05-18','2022-05-22','Privado', 10.51, 'Fondo'
exec post_challenge 'Chal3','Reto escalada Cartago','2022-05-20','2022-05-23','Público', 43.12, 'Altura'
exec post_challenge 'Chal4','Torneo Futbol 5 2022','2022-05-22','2022-05-24','Privado', 19.44, 'Fondo'


exec post_competition 'Comp1','Ruta de los conquistadores','archivo.gpx','2022-05-18','Público','1-3129-9123', 120, 'Act7'
exec post_competition 'Comp2','Iron Man','archivo.gpx','2022-05-20','Público','1-3239-4574', 250, 'Act2'
exec post_competition 'Comp3','IV Escalada Anual Chirripó','archivo.gpx','2022-05-20','Público','1-3239-4574', 175, 'Act3'


exec post_group 'Ciclismo TEC','gabogh99'
exec post_group 'Natación Cartago','gabogh99'
exec post_group 'Futbol Cot','omend'
exec post_group 'Ciclistas San José','dani_08'
exec post_group 'Compra venta Red Velvet','dani_08'


exec post_sponsors '3-1293-1873','Gatorade','1-1359-2598-7','Comp1'
exec post_sponsors '3-1746-4524','Herbalife','1-3423-6398-2','Comp1'
exec post_sponsors '3-2343-6262','CicloBike','1-6146-1375-7','Comp1'
exec post_sponsors '3-5246-4683','Adidas','1-4515-3462-9','Comp2'



insert into dbo.Group_Member (GroupName,MemberID)
values('Ciclismo TEC', 'gabogh99'),
('Ciclismo TEC', 'dani_08'),
('Ciclismo TEC', 'omend'),
('Futbol Cot ', 'gabogh99'),
('Futbol Cot ', 'omend'),
('Compra venta Red Velvet', 'omend'),
('Compra venta Red Velvet', 'dani_08')



insert into dbo.Athlete_In_Challenge
(AthleteID,ChallengeID,Status)
values('gabogh99', 'Chal1','Finalizado'),
('dani_08', 'Chal1','En curso'),
('omend', 'Chal3','En curso'),
('gabogh99', 'Chal3','Finalizado'),
('omend', 'Chal2','Finalizado')


insert into dbo.Activity_In_Challenge
(ActivityID,ChallengeID)
values('Act2', 'Chal1'),
('Act2', 'Chal2'),
('Act4', 'Chal3'),
('Act4', 'Chal2'),
('Act5', 'Chal3')

insert into dbo.Athlete_In_Competition
(AthleteID,CompetitionID,Status,Receipt, Duration)
values('gabogh99', 'Comp1','No aceptado','recibo.pdf','01:57:14'),
('dani_08', 'Comp1','Aceptado','recibo.pdf','02:08:14'),
('omend', 'Comp1','Aceptado','recibo.pdf','02:21:14'),
('gabogh99', 'Comp3','No aceptado','recibo.pdf','02:21:14'),
('omend', 'Comp3','No aceptado','recibo.pdf','02:10:14')

insert into dbo.Athlete_In_Competition
(AthleteID,CompetitionID,Status,Receipt, Duration)
values('omend', 'Comp2','No aceptado','recibo.pdf','02:21:14'),
('dani_08', 'Comp2','Aceptado','recibo.pdf','02:21:14')