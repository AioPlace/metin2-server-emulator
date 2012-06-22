-- ----------------------------
-- Table structure for `account`
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(30) NOT NULL,
  `password` varchar(45) NOT NULL,
  `deletecode` varchar(13) NOT NULL DEFAULT '1234567',
  `status` varchar(8) NOT NULL DEFAULT 'OK',
  `bandate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `empire` tinyint(4) NOT NULL DEFAULT '0',
  `level` tinyint(4) NOT NULL DEFAULT '0',
  `autoloot_expire` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `doubleexp_expire` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `doubledrop_expire` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`id`,`username`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;