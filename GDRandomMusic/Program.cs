using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace GDRandomMusic
{
	public class Program
	{
		public static void Main(string[] _)
		{
			string songsDirectory = "MenuSongs";
			string resourcesDirectory = "Resources";
			string mainExeName = "GeometryDash.exe";

			if (!Directory.Exists(songsDirectory))
			{
				try
				{
					Directory.CreateDirectory(songsDirectory);
				}
				catch (Exception e)
				{
					MessageBox.Show(
						"Не удалось создать папку MenuSongs:\n" + e.Message,
						"Ошибка",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					return;
				}
			}

			var files = Directory.GetFiles(songsDirectory)
						.Where(file => file.EndsWith(".mp3"));
			if (files.Count() == 0)
			{
				MessageBox.Show(
					"Добавьте хотя бы один .mp3 файл в папку MenuSongs",
					"Ошибка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}

			if (!Directory.Exists("resources"))
			{
				MessageBox.Show(
					"Поместите исполняемый файл приложения в папку Geometry Dash",
					"Ошибка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}

			string randomSong = Path.GetFullPath(files.ElementAt(new Random().Next(0, files.Count())));
			try
			{
				File.Copy(randomSong, Path.GetFullPath(Path.Combine(resourcesDirectory, "MenuLoop.mp3")), true);
			}
			catch (Exception e)
			{
				MessageBox.Show(
					"Не удалось скопировать музыку " + randomSong + ":\n" + e.Message,
					"Ошибка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}

			try
			{
				Process p = Process.Start(mainExeName);
				if (p == null)
				{
					throw new Exception();
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(
					"Не удалось запустить Geometry Dash\n" + e.Message,
					"Ошибка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}
	}
}
