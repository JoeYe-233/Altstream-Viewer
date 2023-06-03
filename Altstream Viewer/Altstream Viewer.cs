using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QTPlugin;
using QTPlugin.Interop;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Trinet.Core.IO.Ntfs;
using System.Drawing;

namespace QuizoPlugins
{
	[Plugin( PluginType.Interactive, typeof( Localizer ) )]
	public class Renamer : IBarButton
	{
		private const int COUNT_OF_STRING_RESOURCES = 5;

		private IPluginServer pluginServer;
		private IShellBrowser shellBrowser;
		//private Altstream_Detailed_View altstream_Detailed=new Altstream_Detailed_View();
		private Localizer localizer = new Localizer();


		public void Open( IPluginServer pluginServer, QTPlugin.Interop.IShellBrowser shellBrowser )
		{
			this.pluginServer = pluginServer;
			this.shellBrowser = shellBrowser;
			this.pluginServer.ShortcutKeyPressed += new PluginKeyEventHandler( pluginServer_ShortcutKeyPressed );
		}

		public bool QueryShortcutKeys( out string[] actions )
		{
			actions = localizer.Shortcuts;
			return true;
		}

		public void Close( EndCode endCode )
		{
		}

		public void OnMenuItemClick( MenuType menuType, string menuText, ITab tab )
		{
		}

		public void OnOption()
		{
		}

		public void OnShortcutKeyPressed( int index )
		{
		}




		public System.Drawing.Image GetImage( bool fLarge )
		{
            return fLarge ? Resource.AltstreamViewer_large : Resource.AltstreamViewer_tiny;
        }

		public void OnButtonClick()
		{
			this.GetAltstream();
		}

		public void InitializeItem()
		{
		}

		public bool ShowTextLabel
		{
			get
			{
				return false;
			}
		}

		public string Text
		{
			get
			{
				return this.localizer.Name;
			}
		}

		public bool HasOption
		{
			get
			{
				return false;
			}
		}

		private void pluginServer_ShortcutKeyPressed( object sender, PluginKeyEventArgs e )
		{
            if (!e.Repeat)
            {
                GetAltstream();
            }
        }
        const string ZoneName = "Zone.Identifier";
        private void GetAltstream()
        {
            try
			{
                Address[] adrs;
				this.pluginServer.TryGetSelection(out adrs);

				if(adrs.Length <= 0)
				{
					MessageBox.Show("没有选中任何文件。","注意🤔",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return;
				}

                foreach (QTPlugin.Address Addr in adrs)
                {
					string path = Addr.Path;
                    bool result = FileSystem.AlternateDataStreamExists(path, ZoneName);
                    if (result)//if there is any Zone.Identifier info embedded in the file...
                    {
                        // Clear the read-only attribute, if set:
                        //FileAttributes attributes = File.GetAttributes(path);
                        //if (FileAttributes.ReadOnly == (FileAttributes.ReadOnly & attributes))
                        //{
                        //	attributes &= ~FileAttributes.ReadOnly;
                        //	File.SetAttributes(path, attributes);
                        //}

                        AlternateDataStreamInfo s = FileSystem.GetAlternateDataStream(path, ZoneName, FileMode.Open);
                        using (TextReader reader = s.OpenText())
                        {
                            MessageBox.Show("曾经是在这儿下载的文件:\r\n\r\n" + reader.ReadToEnd(), "找到啦🥰", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //if (result) Console.WriteLine("Process {0}", path);
                    }
                    else
                    {
						//altstream_Detailed.clearAltstreamList();
                        string otherInfo = "";
						//altstream_Detailed.Show();
                        foreach (AlternateDataStreamInfo s in FileSystem.ListAlternateDataStreams(path))
                        {
							otherInfo += s.Name + " (" + s.Size + " 字节)\r\n";//：\r\n" + s.OpenText().ReadToEnd()+"\r\n";
                            //altstream_Detailed.displayAltstreamList(s.Name, s.Size, s.OpenText().ReadToEnd());
                            //altstream_Detailed.AltstreamList.Groups.Add(new ListViewGroup(s.Name + " ( " + string.Format("{0:N}", s.Size) + " 字节)", HorizontalAlignment.Left));
                            //altstream_Detailed.AltstreamList.Groups[s.Name].Items.Add(s.OpenText().ReadToEnd());
                        }
                        MessageBox.Show("没有找到 Zone.Identifier 信息😥\r\n下面是一些其他信息，希望能帮到你:\r\n\r\n" + otherInfo, "没找到😥", MessageBoxButtons.OK, MessageBoxIcon.None);
						
                    }
                }
                //string str = SanitizeNameString( Clipboard.GetText() );
                //if( !String.IsNullOrEmpty( str ) )
                //{
                //Address[] adrs;
                //if( this.pluginServer.TryGetSelection( out adrs ) && adrs.Length == 1 && !String.IsNullOrEmpty( adrs[0].Path ) )
                //{
                //	string path = adrs[0].Path;
                //	string pathTarget;

                //	FileInfo fi = new FileInfo( path );
                //	if( fi.Exists )
                //	{
                //		int index = str.LastIndexOf( "." );
                //		if( index == -1 || index == str.Length - 1 )
                //		{
                //			// no extension in clipboard, append source extension
                //			str += Path.GetExtension( path );
                //		}

                //		pathTarget = Path.GetDirectoryName( path ) + "\\" + str;

                //		if( !File.Exists( pathTarget ) )
                //		{
                //			fi.MoveTo( pathTarget );
                //			return;
                //		}
                //	}
                //	else
                //	{
                //		DirectoryInfo di = new DirectoryInfo( path );
                //		if( di.Exists )
                //		{
                //			pathTarget = Path.GetDirectoryName( path ) + "\\" + str;
                //			if( !Directory.Exists( pathTarget ) )
                //			{
                //				di.MoveTo( pathTarget );
                //				return;
                //			}
                //		}
                //	}

                //}
                //}
            }
			catch
			{
			}

			System.Media.SystemSounds.Beep.Play();
		}

		


	}

	sealed class Localizer : LocalizedStringProvider2
	{
		string[] res;

		public Localizer()
		{
			if( CultureInfo.CurrentCulture.Parent.Name == "zh" )
			{
				res = Resource.zh.Split( new char[] { ';' } );
			}
			else
			{
				res = Resource.en.Split( new char[] { ';' } );
			}
		}

		public override DateTime LastUpdate
		{
			get
			{
				return new DateTime( 2012, 4, 1 );
			}
		}

		public override string SupportURL
		{
			get
			{
				return String.Empty;
			}
		}

		public override string Author
		{
			get
			{
				return "Joe Ye";
			}
		}

		public override string Description
		{
			get
			{
				return res[0];
			}
		}

		public override string Name
		{
			get
			{
				return "Altstream Viewer";
			}
		}

		public override void SetKey( int iKey )
		{
			
		}

		public string[] Shortcuts
		{
			get
			{
				return res;
			}
		}
	}
}
