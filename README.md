# Altstream Viewer

“哎我说，这文件当时我是从哪儿下的来着…？”

---

A QTTabbar plugin to find out where you downloaded the file, by reading out the **Zone.Identifier** information contained in the file's alternate data stream.

*Support files stored on NTFS file system only.

通过查询写在 Zone.Identifier 这个 Alternate Data Stream 里面的数据帮助那些健忘的人们（包括我自己）找回当时是在哪儿下载的文件。仅支持 NTFS 分区

---
## How to install

1. Download latest release and put it in a folder (preferablely [path of QTTabbar]\\Plugins\\)
1. QTTabbar Options -> Plugins -> Install Plugins
1. QTTabbar Options -> Command Bar Buttons -> drag the button to QTCommandBar on any Explorer window

## Usage

1. Select one or more files in any Explorer window
1. Press the button on the QTCommandBar

## How to build

1. fix the reference issue by updating the reference to **QTTabbarLib.dll**
1. build.
---

如本项目对您有所帮助，请帮忙点一个⭐Star 支持一下作者。如有任何问题欢迎提交 issue 与我联系。

包含下列开源项目的代码：

**This project contains source code from the following projects:**

1. [RichardD2/NTFS-Streams](https://github.com/RichardD2/NTFS-Streams) by RichardD2, which provides alternate data stream r/w support.
2. [Official QTTabbar Plugin Sources](http://qttabbar.wikidot.com/plugins) by Quizo, that provides the base code to write a plugin.

So many thanks for all the hardwork of Richard and Quizo!