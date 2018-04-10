# homework3
**1、参考 Fantasy Skybox FREE 构建自己的游戏场景**

- Assets->Import Package **导入所需资源**

- create一个Terrain对象，利用右侧**地形设计工具**设计

![这里写图片描述](http://img.blog.csdn.net/20180410182545281?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvUnVubmVyMXN0/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

- **刷地、种树、种草**快捷方法：
 以种树为例，先选择种树工具，再Edit Trees->Add tree，将预设拖入相应框中便可，之后就可以像造山一样随意选择大小种植。

- **SkyBox**：
Assets->Create->Material,  Inspector->Shader->SkyBox->6 sized, 从资源中的天空素材分别拖到相应的六个面，完成后将材料拖到场景即可

- 调整相机位置

- 效果图：

近景

![这里写图片描述](http://img.blog.csdn.net/20180410183933241?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvUnVubmVyMXN0/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

远景

![这里写图片描述](http://img.blog.csdn.net/20180410190933907?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvUnVubmVyMXN0/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)


**2、写一个简单的总结，总结游戏对象的使用**

- 常见游戏对象
  -  Camera 摄像机：摄像机的位置可以改变人称视角
 
  -  天空盒：利用资源库

  -  3D物体

  -  地形系统：利用资源和地形设计工具

  -  audio音频

  -  光源

  -  游戏资源库
