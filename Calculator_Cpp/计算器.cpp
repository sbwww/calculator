#include <bits/stdc++.h>
#define MAXBUFFER 20			//最长的数字，包括小数位 

using namespace std;

string in_to_post(string s)
{
	string post_s;
	stack<char> sign;
	for(size_t i=0;i<s.length();++i){
		while(isdigit(s[i])||s[i]=='.'){
        		post_s.push_back(s[i]);
			++i;
            		if(!isdigit(s[i])&&s[i]!='.'){
           			post_s.push_back(' ');
					break;
		        }
        	}							
        
		if(sign.empty()){
			sign.push(s[i]);
		}else{
			switch(s[i]){
				case '+':
				case '-':
					while(!sign.empty()&&sign.top()!='('){
						post_s.push_back(sign.top());
						post_s.push_back(' ');
						sign.pop();
					}
					sign.push(s[i]);
					break;

				case '*':
				case '/':
					if(sign.top()=='*'||sign.top()=='/'||sign.top()=='^'){
						post_s.push_back(sign.top());
						post_s.push_back(' ');
						sign.pop();
					}
					sign.push(s[i]);
					break;

				case '^':
					if(sign.top()=='^'){
						post_s.push_back(sign.top());
						post_s.push_back(' ');
						sign.pop();
					}
					sign.push(s[i]);
					break;		

				case '(':
					sign.push(s[i]);
					break;

				case ')':
					while (sign.top()!='('&&!sign.empty()){
						post_s.push_back(sign.top());
						post_s.push_back(' ');
						sign.pop();
					}
					sign.pop();
					break; 
				
				default :
					break;
			}
		}
	}
	while (!sign.empty()){
		post_s.push_back(sign.top());
		post_s.push_back(' ');
		sign.pop();
	}
	return post_s;
}

bool flag_0=true;	//检查分母是否为0

double post_math(string s)
{
	double temp1, temp2;
	stack<double> result;
	char num[MAXBUFFER];
	int k=0;
	double d;
	for(size_t i=0;i<s.length()-1;++i){
		if(s[i]==' '){
			continue;
		}
		bool flag=false;
		while(isdigit(s[i])||s[i]=='.'){
			num[k++]=s[i];
            		num[k]='\0';
		 	if(k>=MAXBUFFER){
				printf("error");
				return -1;
			}
			++i;
            		if(s[i]==' '){
               	 		d=atof(num);
                		result.push(d);
                		k=0;
                		flag=true;
               	 		break;
            		}
       	 	}							//把数字存到num[]中，用atof把num转成浮点数存入d 
		if(flag){
			continue;
		}
		
		temp1=result.top();
		result.pop();
		temp2=result.top();
		result.pop();
		switch(s[i]){
			case '+':
				result.push(0.0+temp2+temp1);
				break;
			case '-':
				result.push(0.0+temp2-temp1);
				break;
			case '*':
				result.push(1.0*temp2*temp1);
				break;
			case '/':
				if(temp1==0){
					cout<<"None"<<endl;
					flag_0=false;
					break;
				}
				result.push(1.0*temp2/temp1);
				break;
			case '^':
				result.push(1.0*pow(temp2,temp1));
				break;
			default:
				break;
		}
		if(!flag_0){
			return 0;
		}
	}
	return result.top();
}

void input_check()
{
	cout<<"请确保使用英文输入法，输入 ( 并回车 来检验"<<endl;
	while(getchar()!='('){
		cout<<"请改为英文输入法"<<endl;
		getchar();
		getchar();
	}
	cout<<"输入法正确"<<endl;
}

bool check_sign(string s)
{
	int cnt1=0,cnt2=0;
	for(size_t i=0;i<s.length();++i){
		if(s[i]=='('){
			++cnt1;
			if(s[i+1]==')'){
				return false;
			}
		}
		if(s[i]==')'){
			++cnt2;
		}
	}
	if(cnt1!=cnt2){
		return false;
	}
	return true;
}

int main()
{
	string a,post_s;
	double result;
	input_check();
	for(;;){
		cout<<"接下来可以输入需要计算的表达式"<<endl;
		cin>>a;
		if(!check_sign(a)){
			cout<<"None"<<endl;
			continue;
		}
		post_s=in_to_post(a);
		cout<<"后缀表达式为:"<<endl;
		cout<<post_s<<endl;
		result=post_math(post_s);
		if(!flag_0){
			cout<<endl;
			continue;
		}
		cout<<"结果为:"<<endl;
		cout<<result<<endl<<endl;
	}
	return 0;
}

