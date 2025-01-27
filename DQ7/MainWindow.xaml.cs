﻿using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DQ7
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Properties.Settings.Default.Save();
		}

		private void Window_PreviewDragOver(object sender, DragEventArgs e)
		{
			e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
		}

		private void Window_Drop(object sender, DragEventArgs e)
		{
			String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];
			if (files == null) return;
			if (!System.IO.File.Exists(files[0])) return;

			if (SaveData.Instance().Open(files[0], false) == false)
			{
				MessageBox.Show("File loading failed");
				return;
			}
			Init();
			MessageBox.Show("File loaded");
		}

		private void MenuItemFileOpen_Click(object sender, RoutedEventArgs e)
		{
			Load(false);
		}

		private void MenuItemFileOpenForce_Click(object sender, RoutedEventArgs e)
		{
			Load(true);
		}

		private void MenuItemFileSave_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void MenuItemFileSaveAs_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == false) return;

			if (SaveData.Instance().SaveAs(dlg.FileName) == true) MessageBox.Show("File saved");
			else MessageBox.Show("File saving failed");
		}

		private void MenuItemExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
		{
			AboutWindow dlg = new AboutWindow();
			dlg.ShowDialog();
		}

		private void ToolBarFileOpen_Click(object sender, RoutedEventArgs e)
		{
			Load(false);
		}

		private void ToolBarFileSave_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void MenuItemBattleMagicCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var magic in (ListBoxCharactor.SelectedItem as Charactor)?.BattleMagics)
			{
				magic.Leam = true;
			}
		}

		private void MenuItemBattleMagicUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var magic in (ListBoxCharactor.SelectedItem as Charactor)?.BattleMagics)
			{
				magic.Leam = false;
			}
		}

		private void MenuItemFieldMagicCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var magic in (ListBoxCharactor.SelectedItem as Charactor)?.FieldMagics)
			{
				magic.Leam = true;
			}
		}

		private void MenuItemFieldMagicUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var magic in (ListBoxCharactor.SelectedItem as Charactor)?.FieldMagics)
			{
				magic.Leam = false;
			}
		}

		private void MenuItemSkillCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var skill in (ListBoxCharactor.SelectedItem as Charactor)?.Skills)
			{
				skill.Leam = true;
			}
		}

		private void MenuItemSkillUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var skill in (ListBoxCharactor.SelectedItem as Charactor)?.Skills)
			{
				skill.Leam = false;
			}
		}

		private void MenuItemJobMax_Click(object sender, RoutedEventArgs e)
		{
			foreach (var job in (ListBoxCharactor.SelectedItem as Charactor)?.Jobs)
			{
				job.Lv = 8;
				job.Exp = 999;
			}
		}

		private void MenuItemJobMin_Click(object sender, RoutedEventArgs e)
		{
			foreach (var job in (ListBoxCharactor.SelectedItem as Charactor)?.Jobs)
			{
				job.Lv = 8;
				job.Exp = 999;
			}
		}

		private void MenuItemPlaceCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var place in (DataContext as DataContext)?.Places)
			{
				place.Visit = true;
			}
		}

		private void MenuItemPlaceUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var place in (DataContext as DataContext)?.Places)
			{
				place.Visit = true;
			}
		}

		private void MenuItemTownMonsterCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var monster in (DataContext as DataContext)?.TownMonsters)
			{
				monster.Exist = true;
			}
		}

		private void MenuItemTownMonsterUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var monster in (DataContext as DataContext)?.TownMonsters)
			{
				monster.Exist = false;
			}
		}
		private void MenuItemMonsterParkPlaceCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var place in (DataContext as DataContext)?.MonsterParkPlaces)
			{
				place.Exist = true;
			}
		}

		private void MenuItemMonsterParkPlaceUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var place in (DataContext as DataContext)?.MonsterParkPlaces)
			{
				place.Exist = false;
			}
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Init();
		}

		private void ButtonCharactorItem_Click(object sender, RoutedEventArgs e)
		{
			CharactorItem item = (sender as Button)?.DataContext as CharactorItem;
			if (item == null) return;
			ItemSelectWindow dlg = new ItemSelectWindow();
			dlg.ID = item.ID;
			dlg.ShowDialog();
			item.ID = dlg.ID;
			item.Count = dlg.ID == 0 ? 0 : 1U;
		}

		private void ButtonBagItem_Click(object sender, RoutedEventArgs e)
		{
			BagItem item = (sender as Button)?.DataContext as BagItem;
			if (item == null) return;
			ItemSelectWindow dlg = new ItemSelectWindow();
			dlg.ID = item.ID;
			dlg.ShowDialog();
			item.Count = dlg.ID == 0 ? 0 : 1U;
			item.ID = dlg.ID;
		}

		private void Load(bool force)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			if (SaveData.Instance().Open(dlg.FileName, force) == false)
			{
				MessageBox.Show("File loading failed");
				return;
			}
			Init();
			MessageBox.Show("File loaded");
		}

		private void Init()
		{
			DataContext = new DataContext();
		}

		private void Save()
		{
			if (SaveData.Instance().Save() == true) MessageBox.Show("File saved");
			else MessageBox.Show("File saving failed");
		}

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged()
        {

        }
    }
}
