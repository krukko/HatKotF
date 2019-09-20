BEGIN TRANSACTION;
CREATE TABLE `Emotions` (
	`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	`name`	TEXT NOT NULL,
	`image`	TEXT,
	`dialogue1`	TEXT,
	`dialogue2`	INTEGER,
	`dialogue3`	INTEGER
);
INSERT INTO `Emotions` VALUES (0,'happy',NULL,'This works now.','My code works!',NULL);
INSERT INTO `Emotions` VALUES (1,'sad',NULL,'My code doesn''t work...','I don''t have any more cookies.',NULL);
INSERT INTO `Emotions` VALUES (2,'angry',NULL,'My code doesn''t work!!!!','RAAAAAAAHHHHHHH!!!!',NULL);
INSERT INTO `Emotions` VALUES (3,'fearful',NULL,'What if the code doesn''t work?!','Did I leave the stove on?',NULL);
INSERT INTO `Emotions` VALUES (4,'surprised',NULL,'It''s Tuesday already?!','Someone is playing this?!',NULL);
INSERT INTO `Emotions` VALUES (5,'disgusted',NULL,'This code is subpar!','What an ugly screen.',NULL);
COMMIT;
