-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1
-- Время создания: Май 16 2020 г., 20:30
-- Версия сервера: 5.5.25
-- Версия PHP: 5.3.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `disks`
--

-- --------------------------------------------------------

--
-- Структура таблицы `collection`
--

CREATE TABLE IF NOT EXISTS `collection` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(20) COLLATE cp1251_ukrainian_ci NOT NULL,
  `title` varchar(20) COLLATE cp1251_ukrainian_ci NOT NULL,
  `company` varchar(20) COLLATE cp1251_ukrainian_ci NOT NULL,
  `release_year` int(4) NOT NULL,
  `type` varchar(20) COLLATE cp1251_ukrainian_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=cp1251 COLLATE=cp1251_ukrainian_ci AUTO_INCREMENT=19 ;

--
-- Дамп данных таблицы `collection`
--

INSERT INTO `collection` (`id`, `code`, `title`, `company`, `release_year`, `type`) VALUES
(1, '55601333852', 'Red Alert 2', 'American Disks', 2006, 'Games'),
(2, '05553581990', 'Enigma', 'CleanMusic', 1990, 'Music'),
(4, '055535813223', 'Half-Life 2', 'R. G. Механики', 2006, 'Games'),
(5, '3266532813223', 'Iron Man ', 'Marvel', 2008, 'Movies'),
(6, '552', 'Red Alert 2', 'American Disks', 2006, 'Games'),
(7, '05553581990', 'Enigma', 'CleanMusic', 1990, 'Music'),
(8, '055535813223', 'Half-Life 2', 'R. G. Механики', 2007, 'Games'),
(9, '3266532813223', 'Iron Man', 'Marvel', 2007, 'Movies'),
(10, '55601333852', 'Red Alert 2', 'American Disks', 2006, 'Games'),
(11, '05553581990', 'Enigma', 'CleanMusic', 1990, 'Music'),
(12, '055535813223', 'Half-Life 2', 'R. G. Механики', 2006, 'Games'),
(13, '3266532813223', 'Iron Man', 'Marvel', 2007, 'Movies'),
(14, '3453454', 'IDK', 'idk', 2020, 'iiddkk'),
(15, '525896601333', 'Find A Way', 'American Disks', 2015, 'Music'),
(16, '07535428990', 'Mambo', 'CleanMusic', 1990, 'Music'),
(17, '2573', 'Red Alert 3', 'WhiteDisks', 2011, 'Games'),
(18, '32623561', 'Iron Man 2', 'Marvel', 2009, 'Movies');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
