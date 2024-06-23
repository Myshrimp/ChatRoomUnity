//各种中心的接口，什么也没有
public interface center_ {

    //资源中心 存放全局可用资源和数据
    //Put输入输出中心 存放所有给值者 可以取出值：
    /*
      public string AI_Input_String;
    public Uri AI_Input_Image;
    public void AI_Input(string s) { }
    public int AI_Input_Type;
     */





    /*
     Center_Put.instance.AI_Input("字符串或者uri内容");
     */
    // void AI_Input(string s) { }//把ai结果通过它交付 文字还是uri都可以



    //向ai输出信息  Center_Put.instance.AI_Output+=方法(string) 
    //采用订阅者模式     //定义一个方法（string）即可






    //Input玩家输入中心  制按钮当前能否（正在）工作，能工作的话和输入输出中心作交互
    //News 消息中心
}
