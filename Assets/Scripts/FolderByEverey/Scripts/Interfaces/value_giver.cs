

//��ֵ��   ͨ��ָ�������ֵ������Ҫ�ľ��Ǹ�����������ѡ
public interface value_giver {
    //int ָ�� giver_pointer;
    //float[] ֵ giver_values;
    //string[] �ַ���ֵ giver_strs;
    string toString();//���ַ�����Ϸ���
    //string str_front,str_back;//�Զ����ַ������
    //bool use_value=false;//ʹ��ֵ�����ַ����������,Ĭ���ַ���

    float getValue();//����ֵ
    string getStr();//�����ַ���
    void uiToPointer(float i);//��uiֵת����ָ��

    //bool isUsing =true; //�Ƿ����ã����ܱ�����


}
