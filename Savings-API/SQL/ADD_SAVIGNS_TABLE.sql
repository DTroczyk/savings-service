CREATE TABLE `Savings` (
  `id` int NOT NULL AUTO_INCREMENT,
  `insertDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `goal` varchar(100) NOT NULL,
  `description` varchar(250) NOT NULL,
  `amount` int NOT NULL,
  `date` date DEFAULT NULL,
  `userId` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;