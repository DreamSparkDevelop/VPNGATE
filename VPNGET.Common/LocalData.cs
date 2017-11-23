using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace VPNGET.Common
{
    public partial class LocalData
    {
        Windows.Storage.ApplicationDataContainer localSetting = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        /// <summary>
        /// 存储本地数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetLocalConfig(Object key, Object value)
        {
            localSetting.Values[key.ToString()] = value;
        }

        /// <summary>
        /// 读取本地数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Object GetLoaclConfig(Object key)
        {
            return localSetting.Values[key.ToString()];
        }

        /// <summary>
        /// 设置复合型数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="CompositeValue"></param>
        public void SetCompositeValue(Object key, Windows.Storage.ApplicationDataCompositeValue CompositeValue)
        {
            localSetting.Values[key.ToString()] = CompositeValue;
        }

        /// <summary>
        /// 读取复合型数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="CompositeValue"></param>
        public Object GetCompositeValue(Object key)
        {
            //Windows.Storage.ApplicationDataCompositeValue Composite = 
            return (Windows.Storage.ApplicationDataCompositeValue)localSetting.Values[key.ToString()];

        }

        /// <summary>
        /// 创建容器
        /// </summary>
        /// <param name="ContainerName">容器的名字</param>
        /// <returns></returns>
        Windows.Storage.ApplicationDataContainer CreatContainer(object ContainerName)
        {
            return localSetting.CreateContainer(ContainerName.ToString(), Windows.Storage.ApplicationDataCreateDisposition.Always);
        }

        /// <summary>
        /// 数据保存到容器中
        /// </summary>
        /// <param name="ContainerName">容器名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetContainerValue(Object ContainerName, Object key, Object value)
        {
            if (!localSetting.Containers.ContainsKey(ContainerName.ToString()))
                CreatContainer(ContainerName.ToString());

            localSetting.Containers[ContainerName.ToString()].Values[key.ToString()] = value;
        }

        /// <summary>
        /// 读取容器内的存储数据
        /// </summary>
        /// <param name="ContainerName">容器名字</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public Object GetValueFromeContainer(Object ContainerName, Object key)
        {
            Object obj = null;
            Boolean isContainer = localSetting.Containers.ContainsKey(ContainerName.ToString());
            if (isContainer)
            {
                obj = localSetting.Containers[ContainerName.ToString()].Values[key.ToString()];
            }
            return obj;
        }

        /// <summary>
        /// 根据指定的key删除存储数据
        /// </summary>
        /// <param name="key"></param>
        public void RemoveConfig(Object key)
        {
            localSetting.Values.Remove(key.ToString());
        }

        /// <summary>
        /// 删除容器
        /// </summary>
        /// <param name="ContainerName">容器名字</param>
        public void RemoveContainer(Object ContainerName)
        {
            localSetting.DeleteContainer(ContainerName.ToString());
        }

        /// <summary>
        /// 选取文件
        /// </summary>
        /// <returns></returns>
        public async Task<StorageFile> SelectFile()
        {
            Windows.Storage.Pickers.FileOpenPicker pick = new Windows.Storage.Pickers.FileOpenPicker();
            pick.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            pick.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            pick.FileTypeFilter.Add(".png");
            pick.FileTypeFilter.Add(".jpg");
            pick.FileTypeFilter.Add(".jpeg");
            StorageFile file = await pick.PickSingleFileAsync();

            return file;
        }

        /// <summary>
        /// 将获取到的文件转成bitmap
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<Windows.UI.Xaml.Media.Imaging.BitmapImage> ConverToBitmapImage(Windows.Storage.StorageFile file)
        {
            var bitmap = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            //Windows.Storage.Streams.IRandomAccessStream fileSreams;
            using (var fileSreams = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                bitmap.SetSource(fileSreams);
            }
            //var fileSream = await file.OpenAsync(FileAccessMode.ReadWrite);
            //var bitmap = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            //bitmap.SetSource(fileSream);
            return bitmap;
        }

        /// <summary>
        /// 将获取到的流转成bitmap
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Windows.UI.Xaml.Media.Imaging.BitmapImage ConverToBitmapImage(IRandomAccessStream stream)
        {
            var bitmap = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            bitmap.SetSource(stream);
            return bitmap;
        }

        /// <summary>
        /// 创建并获取当前文件（临时文件夹）
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public async Task<Windows.Storage.StorageFile> CreatFile(string FileName)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync(FileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            return await storageFolder.GetFileAsync(FileName);
        }

        /// <summary>
        /// 把图片保存到本地临时文件夹中
        /// </summary>
        /// <param name="file"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public async Task SaveFile(StorageFile file, Windows.Storage.Streams.IBuffer buffer)
        {
            await Windows.Storage.FileIO.WriteBufferAsync(file, buffer);
        }
    }
}
