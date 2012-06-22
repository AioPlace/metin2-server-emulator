-- ----------------------------
-- Table structure for `characters`
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `account_id` int(11) unsigned NOT NULL,
  `name` varchar(24) NOT NULL,
  `level` tinyint(3) unsigned NOT NULL,
  `race` tinyint(1) unsigned NOT NULL,
  `job` tinyint(1) unsigned NOT NULL,
  `x` int(11) NOT NULL,
  `y` int(11) NOT NULL,
  `map_index` int(11) unsigned NOT NULL,
  `playtime` int(11) unsigned NOT NULL,
  `str` int(11) unsigned NOT NULL,
  `vit` int(11) unsigned NOT NULL,
  `dex` int(11) unsigned NOT NULL,
  `iq` int(11) unsigned NOT NULL,
  `exp` int(11) unsigned NOT NULL,
  `gold` int(11) unsigned NOT NULL,
  `stat_point` smallint(3) unsigned NOT NULL,
  `skill_point` smallint(3) unsigned NOT NULL,
  `quickslot` tinyblob NOT NULL,
  `armor` int(11) unsigned NOT NULL,
  `hair` int(11) unsigned NOT NULL,
  `skill_level` blob NOT NULL,
  `alignment` int(11) NOT NULL,
  `horse_hp` int(11) unsigned NOT NULL,
  `horse_stamina` int(11) unsigned NOT NULL,
  `horse_name` varchar(12) NOT NULL DEFAULT 'Horse',
  `horse_level` tinyint(2) unsigned NOT NULL,
  `horse_riding` tinyint(1) unsigned NOT NULL,
  `horse_skill_points` smallint(3) unsigned NOT NULL,
  `guild` int(11) NOT NULL DEFAULT '-1',
  PRIMARY KEY (`id`,`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
