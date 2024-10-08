/*
 Navicat Premium Data Transfer

 Source Server         : mysql
 Source Server Type    : MySQL
 Source Server Version : 80031 (8.0.31)
 Source Host           : localhost:3306
 Source Schema         : game1

 Target Server Type    : MySQL
 Target Server Version : 80031 (8.0.31)
 File Encoding         : 65001

 Date: 29/11/2022 12:38:58
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for rank
-- ----------------------------
DROP TABLE IF EXISTS `rank`;
CREATE TABLE `rank`  (
  `userid` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `score` int UNSIGNED NULL DEFAULT 0,
  PRIMARY KEY (`userid`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of rank
-- ----------------------------
INSERT INTO `rank` VALUES ('37075d25-1624-42ed-875b-941e33645da5', 902);
INSERT INTO `rank` VALUES ('45a55188-ccb9-465a-a7dc-825b82f07aee', 1670);
INSERT INTO `rank` VALUES ('505428d0-2165-4f2c-8f99-7a08ec6bdf4e', 282);
INSERT INTO `rank` VALUES ('9f0ac96c-bb40-4c15-b1fd-3552174e8653', 1137);
INSERT INTO `rank` VALUES ('bbe1471a-ad98-4a7c-b896-a3795c3a4fdb', 1961);
INSERT INTO `rank` VALUES ('ee786137-1d51-4cb7-8767-0d4387ebd33b', 237);

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `id` int NOT NULL AUTO_INCREMENT,
  `qudaoid` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `userid` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `blocked` tinyint NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (12, '11', '37075d25-1624-42ed-875b-941e33645da5', 0);
INSERT INTO `users` VALUES (13, '12', '9f0ac96c-bb40-4c15-b1fd-3552174e8653', 0);
INSERT INTO `users` VALUES (14, '13', '45a55188-ccb9-465a-a7dc-825b82f07aee', 0);
INSERT INTO `users` VALUES (15, '21', 'ee786137-1d51-4cb7-8767-0d4387ebd33b', 0);
INSERT INTO `users` VALUES (16, '11111', '66af94c5-28e0-4f84-9c30-1d25a2d7047d', 0);
INSERT INTO `users` VALUES (17, '1111', '599d1c7a-77da-4a1c-a23e-b443d44a0b50', 0);
INSERT INTO `users` VALUES (23, '20', '7cfb0570-24a5-4b5f-9bce-fde5605d058f', 0);
INSERT INTO `users` VALUES (24, '31', 'bbe1471a-ad98-4a7c-b896-a3795c3a4fdb', 0);
INSERT INTO `users` VALUES (25, '41', '505428d0-2165-4f2c-8f99-7a08ec6bdf4e', 0);

-- ----------------------------
-- Table structure for usersinfo
-- ----------------------------
DROP TABLE IF EXISTS `usersinfo`;
CREATE TABLE `usersinfo`  (
  `userid` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `name` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `lv` int NOT NULL DEFAULT 1,
  `region` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `power` int UNSIGNED NOT NULL DEFAULT 60,
  PRIMARY KEY (`userid`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of usersinfo
-- ----------------------------
INSERT INTO `usersinfo` VALUES ('37075d25-1624-42ed-875b-941e33645da5', '张飞', 1, 'Zhejiang', 60);
INSERT INTO `usersinfo` VALUES ('45a55188-ccb9-465a-a7dc-825b82f07aee', '大王', 1, 'Zhejiang', 168);
INSERT INTO `usersinfo` VALUES ('505428d0-2165-4f2c-8f99-7a08ec6bdf4e', 'ok', 1, 'Zhejiang', 100);
INSERT INTO `usersinfo` VALUES ('599d1c7a-77da-4a1c-a23e-b443d44a0b50', 'yy', 1, '', 100);
INSERT INTO `usersinfo` VALUES ('66af94c5-28e0-4f84-9c30-1d25a2d7047d', 'hhudsihs', 1, '', 100);
INSERT INTO `usersinfo` VALUES ('7cfb0570-24a5-4b5f-9bce-fde5605d058f', '二点', 1, 'Zhejiang', 100);
INSERT INTO `usersinfo` VALUES ('9f0ac96c-bb40-4c15-b1fd-3552174e8653', '啊三', 1, 'Zhejiang', 100);
INSERT INTO `usersinfo` VALUES ('bbe1471a-ad98-4a7c-b896-a3795c3a4fdb', '没看', 1, 'Zhejiang', 100);
INSERT INTO `usersinfo` VALUES ('ee786137-1d51-4cb7-8767-0d4387ebd33b', '哈哈', 1, '', 100);
INSERT INTO `usersinfo` VALUES ('f53e9c00-af0d-4344-a74a-a9c003dcf322', '台风', 1, 'Zhejiang', 100);

SET FOREIGN_KEY_CHECKS = 1;
