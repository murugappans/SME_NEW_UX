if(typeof window.RadControlsNamespace=="undefined"){
window.RadControlsNamespace={};
}
if(typeof (window.RadControlsNamespace.DomEventMixin)=="undefined"||typeof (window.RadControlsNamespace.DomEventMixin.Version)==null||window.RadControlsNamespace.DomEventMixin.Version<3){
RadControlsNamespace.DomEventMixin={Version:3,Initialize:function(_1){
_1.CreateEventHandler=this.CreateEventHandler;
_1.AttachDomEvent=this.AttachDomEvent;
_1.DetachDomEvent=this.DetachDomEvent;
_1.DisposeDomEventHandlers=this.DisposeDomEventHandlers;
_1._domEventHandlingEnabled=true;
_1.EnableDomEventHandling=this.EnableDomEventHandling;
_1.DisableDomEventHandling=this.DisableDomEventHandling;
_1.RemoveHandlerRegister=this.RemoveHandlerRegister;
_1.GetHandlerRegister=this.GetHandlerRegister;
_1.AddHandlerRegister=this.AddHandlerRegister;
_1.handlerRegisters=[];
},EnableDomEventHandling:function(){
this._domEventHandlingEnabled=true;
},DisableDomEventHandling:function(){
this._domEventHandlingEnabled=false;
},CreateEventHandler:function(_2,_3){
var _4=this;
return function(e){
if(!_4._domEventHandlingEnabled&&!_3){
return;
}
return _4[_2](e||window.event);
};
},AttachDomEvent:function(_6,_7,_8,_9){
var _a=this.CreateEventHandler(_8,_9);
var _b=this.GetHandlerRegister(_6,_7,_8);
if(_b!=null){
this.DetachDomEvent(_b.Element,_b.EventName,_8);
}
var _c={"Element":_6,"EventName":_7,"HandlerName":_8,"Handler":_a};
this.AddHandlerRegister(_c);
if(_6.addEventListener){
_6.addEventListener(_7,_a,false);
}else{
if(_6.attachEvent){
_6.attachEvent("on"+_7,_a);
}
}
},DetachDomEvent:function(_d,_e,_f){
var _10=null;
var _11="";
if(typeof _f=="string"){
_11=_f;
_10=this.GetHandlerRegister(_d,_e,_11);
if(_10==null){
return;
}
_f=_10.Handler;
}
if(!_d){
return;
}
if(_d.removeEventListener){
_d.removeEventListener(_e,_f,false);
}else{
if(_d.detachEvent){
_d.detachEvent("on"+_e,_f);
}
}
if(_10!=null&&_11!=""){
this.RemoveHandlerRegister(_10);
_10=null;
}
},DisposeDomEventHandlers:function(){
for(var i=0;i<this.handlerRegisters.length;i++){
var _13=this.handlerRegisters[i];
if(_13!=null){
this.DetachDomEvent(_13.Element,_13.EventName,_13.Handler);
}
}
this.handlerRegisters=[];
},RemoveHandlerRegister:function(_14){
try{
var _15=_14.index;
for(var i in _14){
_14[i]=null;
}
this.handlerRegisters[_15]=null;
}
catch(e){
}
},GetHandlerRegister:function(_17,_18,_19){
for(var i=0;i<this.handlerRegisters.length;i++){
var _1b=this.handlerRegisters[i];
if(_1b!=null&&_1b.Element==_17&&_1b.EventName==_18&&_1b.HandlerName==_19){
return this.handlerRegisters[i];
}
}
return null;
},AddHandlerRegister:function(_1c){
_1c.index=this.handlerRegisters.length;
this.handlerRegisters[this.handlerRegisters.length]=_1c;
}};
RadControlsNamespace.DomEvent={};
RadControlsNamespace.DomEvent.PreventDefault=function(e){
if(!e){
return true;
}
if(e.preventDefault){
e.preventDefault();
}
e.returnValue=false;
return false;
};
RadControlsNamespace.DomEvent.StopPropagation=function(e){
if(!e){
return;
}
if(e.stopPropagation){
e.stopPropagation();
}else{
e.cancelBubble=true;
}
};
RadControlsNamespace.DomEvent.GetTarget=function(e){
if(!e){
return null;
}
return e.target||e.srcElement;
};
RadControlsNamespace.DomEvent.GetRelatedTarget=function(e){
if(!e){
return null;
}
return e.relatedTarget||(e.type=="mouseout"?e.toElement:e.fromElement);
};
RadControlsNamespace.DomEvent.GetKeyCode=function(e){
if(!e){
return 0;
}
return e.which||e.keyCode;
};
};if(typeof (window.RadControlsNamespace)=="undefined"){
window.RadControlsNamespace=new Object();
}
RadControlsNamespace.AppendStyleSheet=function(_1,_2,_3){
if(!_3){
return;
}
if(!_1){
document.write("<"+"link"+" rel='stylesheet' type='text/css' href='"+_3+"' />");
}else{
var _4=document.createElement("LINK");
_4.rel="stylesheet";
_4.type="text/css";
_4.href=_3;
document.getElementById(_2+"StyleSheetHolder").appendChild(_4);
}
};
function RadComboItem(){
this.ComboBox=null;
this.ClientID=null;
this.Index=0;
this.Highlighted=false;
this.Enabled=true;
this.Selected=0;
this.Text="";
this.Value="";
this.Attributes=new Array();
}
RadComboItem.prototype.Initialize=function(_5){
for(var _6 in _5){
this[_6]=_5[_6];
}
};
RadComboItem.prototype.Select=function(){
if(this.ComboBox.FireEvent(this.ComboBox.OnClientSelectedIndexChanging,this)===false){
return;
}
var _7=this.ComboBox.GetText();
var _8=this.ComboBox.GetLastSeparatorIndex(_7);
var _9=_7.substring(0,_8+1)+this.Text;
this.ComboBox.SetText(_9);
this.ComboBox.SetValue(this.Value);
this.ComboBox.SelectedItem=this;
this.ComboBox.SelectedIndex=this.Index;
this.Highlight();
this.ComboBox.FireEvent(this.ComboBox.OnClientSelectedIndexChanged,this);
this.ComboBox.PostBack();
};
RadComboItem.prototype.Highlight=function(){
if(!this.Enabled){
return;
}
if(!this.ComboBox.IsTemplated||this.ComboBox.HighlightTemplatedItems){
if(this.ComboBox.HighlightedItem){
this.ComboBox.HighlightedItem.UnHighlight();
}
var _a=document.getElementById(this.ClientID);
if(_a){
if(!this.ComboBox.HighlightedItem){
if(_a.className!=this.ComboBox.ItemCssClassHover){
this.CssClass=_a.className;
}else{
this.CssClass=this.ComboBox.ItemCssClass;
}
}
this.SetCssClass(this.ComboBox.ItemCssClassHover);
}
}
this.ComboBox.HighlightedItem=this;
this.Highlighted=true;
};
RadComboItem.prototype.UnHighlight=function(){
if(!this.ComboBox.IsTemplated||this.ComboBox.HighlightTemplatedItems){
this.SetCssClass(this.CssClass);
}
this.ComboBox.HighlightedItem=null;
this.Highlighted=false;
};
RadComboItem.prototype.ScrollIntoView=function(){
var _b=this.GetDomElement().offsetTop;
var _c=this.GetDomElement().offsetHeight;
var _d=this.ComboBox.DropDownDomElement.scrollTop;
var _e=this.ComboBox.DropDownDomElement.offsetHeight;
if(_b+_c>_d+_e){
this.ComboBox.DropDownDomElement.scrollTop=_b+_c-_e;
}else{
if(_b+_c<=_d){
this.ComboBox.DropDownDomElement.scrollTop=_b;
}
}
};
RadComboItem.prototype.ScrollOnTop=function(){
this.ComboBox.DropDownDomElement.scrollTop=this.GetDomElement().offsetTop;
};
RadComboItem.prototype.NextItem=function(){
return this.ComboBox.Items[this.Index+1];
};
RadComboItem.prototype.GetDomElement=function(){
if(!this.DomElement){
this.DomElement=document.getElementById(this.ClientID);
}
return this.DomElement;
};
RadComboItem.prototype.SetCssClass=function(_f){
this.GetDomElement().className=_f;
};
function RadComboBox(_10,_11,_12){
var _13=window[_11];
if(_13!=null&&typeof (_13.Dispose)!="undefined"){
_13.Dispose();
}
if(window.tlrkComboBoxes==null){
window.tlrkComboBoxes=new Array();
}
this.tlrkComboBoxesIndex=tlrkComboBoxes.length;
tlrkComboBoxes[this.tlrkComboBoxesIndex]=this;
this.Items=new Array();
this.ItemMap=new Object();
this.Created=false;
this.ID=_10;
this.ClientID=_11;
this.TagID=_11;
this.DropDownID=_11+"_DropDown";
this.InputID=_11+"_Input";
this.ImageID=_11+"_Image";
this.DropDownPlaceholderID=_11+"_DropDownPlaceholder";
this.MoreResultsBoxID=_11+"_MoreResultsBox";
this.MoreResultsBoxImageID=_11+"_MoreResultsBoxImage";
this.MoreResultsBoxMessageID=_11+"_MoreResultsBoxMessage";
this.Header=_11+"_Header";
this.Changed=false;
this.Focused=false;
this.InputDomElement=document.getElementById(this.InputID);
this.CachedText=this.OriginalText=this.InputDomElement.value;
this.ImageDomElement=document.getElementById(this.ImageID);
this.DropDownPlaceholder=document.getElementById(this.DropDownPlaceholderID);
this.DropDownDomElement=document.getElementById(this.DropDownID);
this.MoreResultsImageDomElement=document.getElementById(this.MoreResultsBoxImageID);
this.MoreResultsBoxMessageDomElement=document.getElementById(this.MoreResultsBoxMessageID);
this.DomElement=document.getElementById(this.ClientID);
this.ValueHidden=document.getElementById(this.ClientID+"_value");
this.TextHidden=document.getElementById(this.ClientID+"_text");
this.ClientWidthHidden=document.getElementById(this.ClientID+"_clientWidth");
this.ClientHeightHidden=document.getElementById(this.ClientID+"_clientHeight");
this.Enabled=true;
this.DropDownVisible=false;
this.LoadOnDemandUrl=null;
this.HighlightedItem=null;
this.SelectedItem=null;
this.ItemRequestTimeout=300;
this.EnableLoadOnDemand=false;
this.AutoPostBack=false;
this.ShowMoreResultsBox=false;
this.OpenDropDownOnLoad=false;
this.MarkFirstMatch=false;
this.IsCaseSensitive=false;
this.SelectOnTab=true;
this.PostBackReference=null;
this.LoadingMessage="Loading...";
this.ScrollDownImage=null;
this.ScrollDownImageDisabled=null;
this.Overlay=null;
this.RadComboBoxImagePosition="Right";
this.ItemCssClass=null;
this.ItemCssClassHover=null;
this.ItemCssClassDisabled=null;
this.ImageCssClass=null;
this.ImageCssClassHover=null;
this.InputCssClass=null;
this.InputCssClassHover=null;
this.LoadingMessageCssClass="ComboBoxLoadingMessage";
this.AutoCompleteSeparator=null;
this.ExternalCallBackPage=null;
this.OnClientSelectedIndexChanging=null;
this.OnClientDropDownOpening=null;
this.OnClientDropDownClosing=null;
this.OnClientItemsRequesting=null;
this.OnClientSelectedIndexChanged=null;
this.OnClientItemsRequested=null;
this.OnClientKeyPressing=null;
this.OnClientBlur=null;
this.OnClientFocus=null;
this.Skin="Classic";
this.HideTimeoutID=0;
this.RequestTimeoutID=0;
this.IsDetached=false;
this.TextPriorToCallBack=null;
this.AllowCustomText=false;
this.ExpandEffectString=null;
this.HighlightTemplatedItems=false;
this.CausesValidation=false;
this.ClientDataString=null;
this.ShowDropDownOnTextboxClick=true;
this.EnableScreenBoundaryDetection=true;
this.ShowWhileLoading=_12;
this.MoreResultsImageHovered=false;
this.ErrorMessage=null;
this.AfterClientCallBackError=null;
this.PostBackActive=false;
this.SelectedIndex=-1;
this.IsTemplated=false;
this.CurrentText=null;
this.OffsetX=0;
this.OffsetY=0;
this.Disposed=false;
var me=this;
this.DetermineDirection();
this.InputDomElement.setAttribute("autocomplete","off");
this.DropDownPlaceholder.onselectstart=function(){
return false;
};
RadControlsNamespace.DomEventMixin.Initialize(this);
if(this.ImageDomElement){
this.AttachDomEvent(this.ImageDomElement,"click","OnImageClick");
}
this.AttachDomEvent(document,"click","OnDocumentClick");
this.AttachDomEvent(document,"contextmenu","OnDocumentClick");
this.AttachDomEvent(this.InputDomElement,"click","OnInputClick");
this.AttachDomEvent(this.InputDomElement,"mouseover","OnInputOver");
this.AttachDomEvent(this.InputDomElement,"mouseout","OnInputOut");
this.AttachDomEvent(this.InputDomElement,"keydown","OnKeyDown");
this.AttachDomEvent(this.InputDomElement,"focus","OnFocus");
if(navigator.userAgent.match(/MSIE/i)==null){
this.AttachDomEvent(this.InputDomElement,"input","OnInputChange");
}
this.AttachDomEvent(this.InputDomElement,"propertychange","OnInputPropertyChange");
this.AttachDomEvent(this.DropDownPlaceholder,"mouseover","OnDropDownOver");
this.AttachDomEvent(this.DropDownPlaceholder,"mouseout","OnDropDownOut");
this.AttachDomEvent(this.DropDownPlaceholder,"click","OnDropDownClick");
if(this.MoreResultsImageDomElement){
this.AttachDomEvent(this.MoreResultsImageDomElement,"mouseover","OnMoreResultsImageOver");
this.AttachDomEvent(this.MoreResultsImageDomElement,"mouseout","OnMoreResultsImageOut");
this.AttachDomEvent(this.MoreResultsImageDomElement,"click","OnMoreResultsImageClick");
}
if(typeof (RadCallbackNamespace)!="undefined"){
window.setTimeout(function(){
if(me.Disposed){
return;
}
me.FixUp(me.InputDomElement,true);
},100);
}else{
if(window.addEventListener){
if(window.opera){
this.AttachDomEvent(window,"load","OnWindowLoad");
}else{
this.OnWindowLoad();
}
}else{
if(document.getElementById(this.ClientID).offsetWidth==0){
this.AttachDomEvent(window,"load","OnWindowLoad");
}else{
this.OnWindowLoad();
}
}
}
this.AttachDomEvent(window,"resize","OnWindowResize");
this.AttachDomEvent(window,"unload","Dispose");
}
RadComboBox.prototype.OnWindowResize=function(){
if(this.DropDownVisible){
this.PositionDropDown();
}
};
RadComboBox.prototype.Initialize=function(_15,_16){
this.LoadConfiguration(_15);
if(!this.Enabled){
this.Disable();
}
this.CreateItems(_16);
this.InitCssNames();
if(this.OpenDropDownOnLoad){
this.AttachDomEvent(window,"load","OpenOnLoad");
}
};
RadComboBox.prototype.OpenOnLoad=function(){
this.FixUp(this.InputDomElement,false);
this.ShowDropDown();
};
RadComboBox.prototype.OnWindowLoad=function(){
this.FixUp(this.InputDomElement,true);
};
RadComboBox.Keys={Shift:16,Escape:27,Up:38,Down:40,Left:37,Right:39,Enter:13,Tab:9,Space:32,PageUp:33,Del:46,F1:112,F12:123};
RadComboBox.prototype.FireEvent=function(_17,_18,_19,_1a){
if(!_17){
return;
}
var _1b=_17.lastIndexOf(")");
if(_1b==_17.length-1){
return eval(_17);
}
RadComboBoxGlobalFirstParam=_18;
RadComboBoxGlobalSecondParam=_19;
RadComboBoxGlobalThirdParam=_1a;
var s=_17;
s=s+"(RadComboBoxGlobalFirstParam";
s=s+",RadComboBoxGlobalSecondParam";
s=s+",RadComboBoxGlobalThirdParam";
s=s+");";
return eval(s);
};
RadComboBox.prototype.PostBack=function(){
if(this.PostBackActive){
return;
}
this.PostBackActive=true;
if(this.AutoPostBack){
if(this.CausesValidation){
if(typeof (WebForm_DoPostBackWithOptions)!="function"&&!(typeof (Page_ClientValidate)!="function"||Page_ClientValidate())){
return;
}
}
eval(this.PostBackReference);
this.PostBackActive=false;
}
};
RadComboBox.prototype.SelectFirstMatch=function(){
var _1d=this.FindItemToSelect();
if(_1d&&_1d.Enabled){
_1d.Highlight();
this.SelectedItem=_1d;
}
};
RadComboBox.prototype.SelectText=function(_1e,_1f){
if(this.InputDomElement.createTextRange){
var _20=this.InputDomElement.createTextRange();
if(_1e==0&&_1f==0){
_20.collapse(true);
return;
}
_20.moveStart("character",_1e);
_20.moveEnd("character",_1f);
_20.select();
}else{
this.InputDomElement.setSelectionRange(_1e,_1e+_1f);
}
};
RadComboBox.prototype.OnInputClick=function(){
this.SelectFirstMatch();
this.SelectText(0,this.GetText().length);
if(this.ShowDropDownOnTextboxClick&&!this.DropDownVisible){
this.ShowDropDown();
}
};
RadComboBox.prototype.OnInputPropertyChange=function(){
if(event.propertyName=="value"){
var _21=this.GetText();
if(this.CachedText!=_21){
this.CachedText=_21;
this.OnInputChange();
}
}
};
RadComboBox.prototype.OnInputChange=function(){
this.SetValue("");
this.TextHidden.value=this.InputDomElement.value;
if(this.EnableLoadOnDemand&&!this.SuppressChange){
var me=this;
if(this.RequestTimeoutID>0){
window.clearTimeout(this.RequestTimeoutID);
this.RequestTimeoutID=0;
}
if(!this.DropDownVisible){
this.ShowDropDown(!this.ShowDropDownOnTextBoxClick);
}
this.RequestTimeoutID=window.setTimeout(function(){
me.RequestItems(me.GetText(),false);
},this.ItemRequestTimeout);
return;
}
if(!this.SuppressChange&&this.ShouldHighlight()){
this.HighlightMatches();
}
};
RadComboBox.prototype.OnImageClick=function(){
if(!this.DropDownVisible){
this.SelectFirstMatch();
}
this.ToggleDropDown();
};
RadComboBox.prototype.OnDocumentClick=function(e){
if(!e){
e=event;
}
var _24=e.target||e.srcElement;
while(_24.nodeType!==9){
if(_24.parentNode==null||_24==this.DomElement||_24==this.DropDownPlaceholder){
return;
}
_24=_24.parentNode;
}
if(this.DropDownVisible){
this.HideDropDown();
}
if(this.Focused){
this.RaiseClientBlur();
this.SelectItemOnBlur();
this.Focused=false;
}
};
RadComboBox.prototype.SelectItemOnBlur=function(){
var _25=this.FindItemToSelect();
if(!_25&&!this.AllowCustomText&&this.Items.length>0){
if(this.MarkFirstMatch){
if(this.GetText()==""){
this.SetText(this.OriginalText);
}
this.HighlightMatches();
this.SelectText(0,0);
_25=this.HighlightedItem;
}
}
this.PerformSelect(_25);
};
RadComboBox.prototype.FindItemToSelect=function(){
var _26=this.FindItemByValueInternal(this.GetValue());
if(!_26){
_26=this.FindItemByText(this.GetText());
}
return _26;
};
RadComboBox.prototype.OnMoreResultsImageOver=function(){
this.MoreResultsImageDomElement.style.cursor="pointer";
this.MoreResultsImageDomElement.src=this.ScrollDownImage;
this.MoreResultsImageHovered=true;
};
RadComboBox.prototype.OnMoreResultsImageOut=function(){
this.MoreResultsImageDomElement.style.cursor="default";
this.MoreResultsImageDomElement.src=this.ScrollDownImageDisabled;
this.MoreResultsImageHovered=false;
};
RadComboBox.prototype.OnMoreResultsImageClick=function(){
this.RequestItems(this.GetText(),true);
};
RadComboBox.prototype.OnDropDownOver=function(e){
var _28=this.GetEventTarget(e);
var _29=this.FindNearestItem(_28);
if(_29){
_29.Highlight();
}
};
RadComboBox.prototype.OnDropDownOut=function(e){
if(!e){
e=event;
}
var _2b;
try{
_2b=e.toElement||e.relatedTarget||e.fromElement;
while(_2b.nodeType!==9){
if(_2b.parentNode==this.DropDownDomElement){
return;
}
_2b=_2b.parentNode;
}
}
catch(e){
}
if(this.HighlightedItem){
this.HighlightedItem.UnHighlight();
}
};
RadComboBox.prototype.OnInputOver=function(e){
this.InputDomElement.className=this.InputCssClassHover;
};
RadComboBox.prototype.OnInputOut=function(e){
this.InputDomElement.className=this.InputCssClass;
};
RadComboBox.prototype.OnDropDownClick=function(e){
var _2f=this.GetEventTarget(e);
var _30=this.FindNearestItem(_2f);
if(!_30||!_30.Enabled){
return;
}
this.HideDropDown();
this.PerformSelect(_30);
};
RadComboBox.prototype.GetEventTarget=function(e){
return e.target||e.srcElement;
};
RadComboBox.prototype.FindNearestItem=function(_32){
while(_32.nodeType!==9){
if(_32.parentNode==this.DropDownDomElement){
return this.ItemMap[_32.id];
}
_32=_32.parentNode;
}
return null;
};
RadComboBox.prototype.GetViewPortSize=function(){
var _33=0;
var _34=0;
var _35=document.body;
if(window.innerWidth){
_33=window.innerWidth;
_34=window.innerHeight;
}else{
if(document.compatMode&&document.compatMode=="CSS1Compat"){
_35=document.documentElement;
}
_33=_35.clientWidth;
_34=_35.clientHeight;
}
_33+=_35.scrollLeft;
_34+=_35.scrollTop;
return {width:_33-6,height:_34-6};
};
RadComboBox.prototype.GetElementPosition=function(el){
var _37=null;
var pos={x:0,y:0};
var box;
if(el.getBoundingClientRect){
box=el.getBoundingClientRect();
var _3a=document.documentElement.scrollTop||document.body.scrollTop;
var _3b=document.documentElement.scrollLeft||document.body.scrollLeft;
pos.x=box.left+_3b-2;
pos.y=box.top+_3a-2;
return pos;
}else{
if(document.getBoxObjectFor){
box=document.getBoxObjectFor(el);
pos.x=box.x-1;
pos.y=box.y-1;
}else{
pos.x=el.offsetLeft;
pos.y=el.offsetTop;
_37=el.offsetParent;
if(_37!=el){
while(_37){
pos.x+=_37.offsetLeft;
pos.y+=_37.offsetTop;
_37=_37.offsetParent;
}
}
}
}
if(window.opera){
_37=el.offsetParent;
while(_37&&_37.tagName!="BODY"&&_37.tagName!="HTML"){
pos.x-=_37.scrollLeft;
pos.y-=_37.scrollTop;
_37=_37.offsetParent;
}
}else{
_37=el.parentNode;
while(_37&&_37.tagName!="BODY"&&_37.tagName!="HTML"){
pos.x-=_37.scrollLeft;
pos.y-=_37.scrollTop;
_37=_37.parentNode;
}
}
return pos;
};
RadComboBox.prototype.Dispose=function(){
if(this.Disposed){
return;
}
if(this.RequestTimeoutID>0){
window.clearTimeout(this.RequestTimeoutID);
this.RequestTimeoutID=0;
}
this.HideDropDown();
if(this.Overlay&&this.Overlay.parentNode){
this.Overlay.parentNode.removeChild(this.Overlay);
this.Overlay=null;
}
if(this.LoadingDiv){
if(this.LoadingDiv.parentNode){
this.LoadingDiv.parentNode.removeChild(this.LoadingDiv);
}
this.LoadingDiv=null;
}
if(this.DropDownPlaceholder!=null&&this.DropDownPlaceholder.parentNode!=null){
try{
this.DropDownPlaceholder.parentNode.removeChild(this.DropDownPlaceholder);
}
catch(e){
}
}
this.DisposeDomEventHandlers();
this.InputDomElement=null;
this.ImageDomElement=null;
this.DropDownPlaceholder=null;
this.DropDownDomElement=null;
this.MoreResultsImageDomElement=null;
this.MoreResultsBoxMessageDomElement=null;
this.DomElement=null;
this.ValueHidden=null;
this.ClientWidthHidden=null;
this.ClientHeightHidden=null;
tlrkComboBoxes[this.tlrkComboBoxesIndex]=null;
this.Disposed=true;
};
RadComboBox.prototype.LoadConfiguration=function(_3c){
for(var _3d in _3c){
this[_3d]=_3c[_3d];
}
};
RadComboBox.prototype.InitCssNames=function(){
this.ItemCssClass="ComboBoxItem_"+this.Skin;
this.ItemCssClassHover="ComboBoxItemHover_"+this.Skin;
this.ItemCssClassDisabled="ComboBoxItemDisabled_"+this.Skin;
this.ImageCssClass="ComboBoxImage_"+this.Skin;
this.ImageCssClassHover="ComboBoxImageHover_"+this.Skin;
this.InputCssClass="ComboBoxInput_"+this.Skin;
this.InputCssClassHover="ComboBoxInputHover_"+this.Skin;
this.LoadingMessageCssClass="ComboBoxLoadingMessage_"+this.Skin;
};
RadComboBox.prototype.FindParentForm=function(){
var _3e=document.getElementById(this.TagID);
while(_3e.tagName!="FORM"){
_3e=_3e.parentNode;
}
return _3e;
};
RadComboBox.prototype.DropDownRequiresForm=function(){
var _3f=this.DropDownPlaceholder.getElementsByTagName("input");
return _3f.length>0;
};
RadComboBox.prototype.DetachDropDown=function(){
if((!document.readyState||document.readyState=="complete")&&(!this.IsDetached)){
var _40=document.body;
if(this.DropDownRequiresForm()){
_40=this.FindParentForm();
}
this.DropDownPlaceholder.parentNode.removeChild(this.DropDownPlaceholder);
this.DropDownPlaceholder.style.marginLeft="0";
_40.insertBefore(this.DropDownPlaceholder,_40.firstChild);
this.IsDetached=true;
}
};
RadComboBox.prototype.CreateItems=function(_41){
for(var i=0;i<_41.length;i++){
var _43=new RadComboItem();
_43.ComboBox=this;
_43.Index=this.Items.length;
_43.Initialize(_41[i]);
if(_43.Selected){
this.SelectedItem=_43;
}
this.ItemMap[_43.ClientID]=_43;
this.Items[this.Items.length]=_43;
}
};
RadComboBox.prototype.ShowOverlay=function(x,y){
if(document.readyState&&document.readyState!="complete"){
return;
}
if(!document.all||window.opera){
return;
}
if(this.Overlay==null){
this.Overlay=document.createElement("iframe");
this.Overlay.src="javascript:''";
this.Overlay.id=this.ClientID+"_Overlay";
this.Overlay.frameBorder=0;
this.Overlay.style.position="absolute";
this.Overlay.style.display="none";
this.DetachDropDown();
this.DropDownPlaceholder.parentNode.insertBefore(this.Overlay,this.DropDownPlaceholder);
this.Overlay.style.zIndex=this.DropDownPlaceholder.style.zIndex-1;
}
this.Overlay.style.left=x;
this.Overlay.style.top=y;
this.Overlay.style.width=this.DropDownPlaceholder.offsetWidth+"px";
this.Overlay.style.height=this.DropDownPlaceholder.offsetHeight+"px";
this.Overlay.style.display="block";
};
RadComboBox.prototype.HideOverlay=function(){
if(!document.all||window.opera){
return;
}
if(this.Overlay!=null){
this.Overlay.style.display="none";
}
};
RadComboBox.prototype.DetermineDirection=function(){
var el=document.getElementById(this.ClientID+"_wrapper");
while(el.tagName.toLowerCase()!="html"){
if(el.dir){
this.RightToLeft=(el.dir.toLowerCase()=="rtl");
return;
}
if(el.style.direction){
this.RightToLeft=(el.style.direction.toLowerCase()=="rtl");
return;
}
el=el.parentNode;
}
this.RightToLeft=false;
};
RadComboBox.prototype.PositionDropDown=function(){
this.DetachDropDown();
var _47=this.DomElement.firstChild;
var _48=this.GetElementPosition(_47);
if(this.ExpandEffectString!=null&&document.all){
try{
this.DropDownPlaceholder.style.filter=this.ExpandEffectString;
this.DropDownPlaceholder.filters[0].Apply();
this.DropDownPlaceholder.filters[0].Play();
}
catch(e){
}
}
this.DropDownPlaceholder.style.position="absolute";
this.DropDownPlaceholder.style.top=_48.y+this.OffsetY+this.InputDomElement.offsetHeight+"px";
this.DropDownPlaceholder.style.left=_48.x+this.OffsetX+"px";
this.DropDownPlaceholder.style.display="block";
var _49=this.DropDownPlaceholder.offsetWidth;
var _4a=this.DomElement.offsetWidth;
if(this.DropDownPlaceholder.style.width!=""&&_49<_4a){
_4a=_49;
}
this.DropDownPlaceholder.style.width=_4a+"px";
var _4b=this.DropDownPlaceholder.offsetWidth-_4a;
if(_4b>0&&_4b<_4a){
this.DropDownPlaceholder.style.width=_4a-_4b+"px";
}
if(this.RightToLeft){
this.DropDownPlaceholder.dir="rtl";
}
if(this.EnableScreenBoundaryDetection){
var _4c=this.GetViewPortSize();
if(this.ElementOverflowsBottom(_4c,this.DropDownPlaceholder,this.InputDomElement)){
var y=_48.y-this.DropDownPlaceholder.offsetHeight;
if(y>=0){
this.DropDownPlaceholder.style.top=y+"px";
}
}
}
this.ShowOverlay(this.DropDownPlaceholder.style.left,this.DropDownPlaceholder.style.top);
this.DropDownVisible=true;
};
RadComboBox.prototype.ShowDropDown=function(_4e){
if(this.FindNotVisibleParent(this.InputDomElement)){
return;
}
if(this.FireEvent(this.OnClientDropDownOpening,this)===false){
return;
}
this.PositionDropDown();
this.InputDomElement.focus();
if(this.EnableLoadOnDemand&&this.Items.length==0&&!_4e){
this.RequestItems(this.GetText(),false);
}
};
RadComboBox.prototype.FindNotVisibleParent=function(_4f){
while(_4f.nodeType!==9){
if(_4f.style&&_4f.style.display=="none"){
return true;
}
_4f=_4f.parentNode;
}
return false;
};
RadComboBox.prototype.ClearItems=function(){
this.Items=[];
this.ItemMap=new Object();
this.DropDownDomElement.innerHTML="";
};
RadComboBox.prototype.RequestItems=function(_50,_51){
if(this.Disposed){
return;
}
if(this.FireEvent(this.OnClientItemsRequesting,this,_50,_51)==false){
return;
}
if(!this.LoadingDiv){
this.LoadingDiv=document.createElement("div");
this.LoadingDiv.className=this.LoadingMessageCssClass;
this.LoadingDiv.id=this.ClientID+"_LoadingDiv";
this.LoadingDiv.innerHTML=this.LoadingMessage;
this.DropDownDomElement.insertBefore(this.LoadingDiv,this.DropDownDomElement.firstChild);
}
var _52=this.GetAjaxUrl(_50,this.GetText(),this.GetValue(),_51);
var _53=this.CreateXmlHttpRequest();
var me=this;
_53.onreadystatechange=function(){
if(_53.readyState!=4){
return;
}
if(!me.Disposed){
me.OnCallBackResponse(_53,_51,_50,_52);
}
};
_53.open("GET",_52,true);
_53.setRequestHeader("Content-Type","application/json; charset=utf-8");
_53.send("");
};
RadComboBox.prototype.OnCallBackResponse=function(_55,_56,_57,_58){
if(this.LoadingDiv){
if(this.LoadingDiv.parentNode){
this.LoadingDiv.parentNode.removeChild(this.LoadingDiv);
}
this.LoadingDiv=null;
}
if(_55.status==500){
if(this.FireEvent(this.AfterClientCallBackError,this)===false){
return;
}
alert("RadComboBox: Server error in the ItemsRequested event handler, press ok to view the result.");
var _59;
this.Dispose();
if(this.ErrorMessage){
_59=this.ErrorMessage;
}else{
_59=_55.responseText;
}
document.body.innerHTML=_59;
return;
}
if(_55.status==404){
if(this.FireEvent(this.AfterClientCallBackError,this)===false){
return;
}
alert("RadComboBox: Load On Demand Page not found: "+_58);
this.Dispose();
var _59;
if(this.ErrorMessage){
_59=this.ErrorMessage;
}else{
_59="RadComboBox: Load On Demand Page not found: "+_58+"<br/>";
_59+="Please, try using ExternalCallBackPage to map to the exact location of the callbackpage you are using.";
}
document.body.innerHTML=_59;
return;
}
try{
eval("var callBackData = "+_55.responseText+";");
}
catch(e){
if(this.FireEvent(this.AfterClientCallBackError,this)===false){
return;
}
alert("RadComboBox: load on demand callback error. Press Enter for more information");
this.Dispose();
var _59;
if(this.ErrorMessage){
_59=this.ErrorMessage;
}else{
_59="If RadComboBox is not initially visible on your ASPX page, you may need to use streamers (the ExternallCallBackPage property)";
_59+="<br/>Please, read our online documentation on this problem for details";
_59+="<br/><a href='http://www.telerik.com/help/radcombobox/v2%5FNET2/?combo_externalcallbackpage.html'>http://www.telerik.com/help/radcombobox/v2%5FNET2/combo_externalcallbackpage.html</a>";
}
document.body.innerHTML=_59;
return;
}
if(this.GetText()!=callBackData.Text){
this.RequestItems(this.GetText(),_56);
return;
}
if(!_56){
this.ClearItems();
}
this.SelectedItem=null;
this.HighlightedItem=null;
var _5a=this.Items.length;
if(_56){
for(var i=0;i<this.Items.length;i++){
this.Items[i].DomElement=null;
}
}
this.CreateItems(callBackData.Items);
if(_56){
this.DropDownDomElement.innerHTML+=callBackData.DropDownHtml;
if(this.Items[_5a+1]!=null){
this.Items[_5a+1].ScrollIntoView();
}
}else{
this.DropDownDomElement.innerHTML=callBackData.DropDownHtml;
}
if(this.ShowMoreResultsBox){
this.MoreResultsBoxMessageDomElement.innerHTML=callBackData.Message;
}
this.FireEvent(this.OnClientItemsRequested,this,callBackData.Text,_56);
if(this.ShouldHighlight()){
this.HighlightMatches();
}
};
RadComboBox.prototype.CreateXmlHttpRequest=function(){
if(typeof (XMLHttpRequest)!="undefined"){
return new XMLHttpRequest();
}
if(typeof (ActiveXObject)!="undefined"){
return new ActiveXObject("Microsoft.XMLHTTP");
}
};
RadComboBox.prototype.ClearSelection=function(){
this.SetText("");
this.SetValue("");
this.SelectedItem=null;
this.HighLightedItem=null;
};
RadComboBox.prototype.GetAjaxUrl=function(_5c,_5d,_5e,_5f){
_5c=_5c.replace(/'/g,"&squote");
var url=window.unescape(this.LoadOnDemandUrl)+"&text="+this.EncodeURI(_5c);
url=url+"&comboText="+this.EncodeURI(_5d);
url=url+"&comboValue="+this.EncodeURI(_5e);
url=url+"&skin="+this.EncodeURI(this.Skin);
if(_5f){
url=url+"&itemCount="+this.Items.length;
}
if(this.ExternalCallBackPage!=null){
url=url+"&external=true";
}
if(this.ClientDataString!=null){
url+="&clientDataString="+this.EncodeURI(this.ClientDataString);
}
url=url+"&timeStamp="+encodeURIComponent((new Date()).getTime());
return url;
};
RadComboBox.prototype.EncodeURI=function(s){
if(typeof (encodeURIComponent)!="undefined"){
return encodeURIComponent(this.EscapeQuotes(s));
}
if(escape){
return escape(this.EscapeQuotes(s));
}
};
RadComboBox.prototype.EscapeQuotes=function(_62){
if(typeof (_62)!="number"){
return _62.replace(/'/g,"&squote");
}
};
RadComboBox.prototype.ToggleDropDown=function(){
if(this.DropDownVisible){
this.HideDropDown();
}else{
this.ShowDropDown();
if(this.HighlightedItem){
this.HighlightedItem.ScrollIntoView();
}
}
};
RadComboBox.prototype.HideDropDown=function(){
if(this.FireEvent(this.OnClientDropDownClosing,this)===false){
return;
}
this.DropDownPlaceholder.style.display="none";
this.HideOverlay();
this.DropDownVisible=false;
};
RadComboBox.prototype.SetText=function(_63){
this.SuppressChange=true;
this.InputDomElement.value=_63;
this.TextHidden.value=_63;
this.ValueHidden.value="";
if(this.InputDomElement.fireEvent&&document.createEventObject){
var _64=document.createEventObject();
this.InputDomElement.fireEvent("onchange",_64);
}else{
if(this.InputDomElement.dispatchEvent){
var _65=true;
var _64=document.createEvent("HTMLEvents");
_64.initEvent("change",_65,true);
this.InputDomElement.dispatchEvent(_64);
}
}
this.SuppressChange=false;
};
RadComboBox.prototype.SetValue=function(_66){
this.ValueHidden.value=_66;
};
RadComboBox.prototype.GetValue=function(){
return this.ValueHidden.value;
};
RadComboBox.prototype.PerformSelect=function(_67){
if(_67&&_67!=this.SelectedItem&&!this.EnableLoadOnDemand){
_67.Select();
return;
}
if(_67&&_67==this.SelectedItem&&this.GetLastWord(this.GetText())!=_67.Text&&this.AllowCustomText){
this.SetText(_67.Text);
return;
}
if(_67&&_67==this.SelectedItem){
return;
}
if(_67&&this.OriginalText!=_67.Text){
_67.Select();
return;
}
if(_67&&(!this.SelectedItem||this.SelectedItem.Value!=_67.Value)){
_67.Select();
return;
}
if(this.OriginalText!=this.GetText()){
if(this.HighlightedItem){
this.HighlightedItem.UnHighlight();
}
this.PostBack();
}
};
RadComboBox.prototype.OnKeyDown=function(e){
if(!e){
e=event;
}
this.Changed=true;
this.FireEvent(this.OnClientKeyPressing,this,e);
var _69=e.keyCode||e.which;
this.LastKeyCode=_69;
if(_69==RadComboBox.Keys.Escape&&this.DropDownVisible){
this.HideDropDown();
return;
}
if(_69===RadComboBox.Keys.Enter){
this.HideDropDown();
this.PerformSelect(this.HighlightedItem);
e.returnValue=false;
if(e.preventDefault){
e.preventDefault();
}
return;
}else{
if(_69===RadComboBox.Keys.Down){
e.returnValue=false;
if(e.altKey){
this.ToggleDropDown();
return;
}
this.HighlightNextItem();
return;
}else{
if(_69===RadComboBox.Keys.Up){
e.returnValue=false;
if(e.altKey){
this.ToggleDropDown();
return;
}
this.HighlightPreviousItem();
return;
}else{
if(_69===RadComboBox.Keys.Tab){
this.HideDropDown();
this.RaiseClientBlur();
this.SelectItemOnBlur();
this.Focused=false;
return;
}
}
}
}
if(_69==RadComboBox.Keys.Left||_69==RadComboBox.Keys.Right){
return;
}
};
RadComboBox.prototype.GetLastWord=function(_6a){
var _6b=-1;
if(this.AutoCompleteSeparator!=null){
_6b=this.GetLastSeparatorIndex(_6a);
}
var _6c=_6a.substring(_6b+1,_6a.length);
return _6c;
};
RadComboBox.prototype.GetLastSeparatorIndex=function(_6d){
var _6e=-1;
if(!this.AutoCompleteSeparator){
return _6e;
}
for(var i=0;i<this.AutoCompleteSeparator.length;i++){
var _70=this.AutoCompleteSeparator.charAt(i);
var _71=_6d.lastIndexOf(_70);
if(_71>_6e){
_6e=_71;
}
}
return _6e;
};
RadComboBox.prototype.GetLastSeparator=function(_72){
if(!this.AutoCompleteSeparator){
return null;
}
var _73=this.GetLastSeparatorIndex(_72);
return _72.charAt(_73);
};
RadComboBox.prototype.ShouldHighlight=function(){
if(this.LastKeyCode<RadComboBox.Keys.Space){
return false;
}
if(this.LastKeyCode>=RadComboBox.Keys.PageUp&&this.LastKeyCode<=RadComboBox.Keys.Del){
return false;
}
if(this.LastKeyCode>=RadComboBox.Keys.F1&&this.LastKeyCode<=RadComboBox.Keys.F12){
return false;
}
return true;
};
RadComboBox.prototype.HighlightMatches=function(){
if(!this.MarkFirstMatch){
return;
}
var _74=this.GetText();
var _75=this.GetLastWord(_74);
if(this.GetLastSeparator(_74)==_74.charAt(_74.length-1)){
return;
}
var _76=this.FindFirstMatch(_75);
if(this.HighlightedItem){
this.HighlightedItem.UnHighlight();
}
if(!_76){
if(!this.AllowCustomText){
if(_74){
var _77=this.GetLastSeparatorIndex(_74);
if(_77<_74.length-1){
this.SetText(_74.substring(0,_74.length-1));
this.HighlightMatches();
}
}
}
return;
}
_76.Highlight();
_76.ScrollOnTop();
var _77=this.GetLastSeparatorIndex(_74);
var _78=_74.substring(0,_77+1)+_76.Text;
if(_74!=_78){
this.SetText(_78);
}
this.SetValue(_76.Value);
var _79=_77+_75.length+1;
var _7a=_78.length-_79;
this.SelectText(_79,_7a);
};
RadComboBox.prototype.FindFirstMatch=function(_7b){
if(!_7b){
return null;
}
for(var i=0;i<this.Items.length;i++){
if(this.Items[i].Text.length<_7b.length){
continue;
}
if(this.Items[i].Enabled==false){
continue;
}
var _7d=this.Items[i].Text.substring(0,_7b.length);
if(!this.IsCaseSensitive){
if(_7d.toLowerCase()==_7b.toLowerCase()){
return this.Items[i];
}
}else{
if(_7d==_7b){
return this.Items[i];
}
}
}
return null;
};
RadComboBox.prototype.OnFocus=function(e){
if(this.Focused){
return;
}
if(!e){
e=event;
}
this.Focused=true;
this.FireEvent(this.OnClientFocus,this);
};
RadComboBox.prototype.RaiseClientBlur=function(){
if(this.Focused){
this.FireEvent(this.OnClientBlur,this);
}
};
RadComboBox.prototype.FindNextAvailableIndex=function(_7f){
for(var i=_7f;i<this.Items.length;i++){
if(this.Items[i].Enabled){
return i;
}
}
return this.Items.length;
};
RadComboBox.prototype.FindPrevAvailableIndex=function(_81){
if(this.Items.length<1){
return -1;
}
for(var i=_81;i>=0;i--){
if(this.Items[i].Enabled){
return i;
}
}
return -1;
};
RadComboBox.prototype.HighlightNextItem=function(){
var _83=this.HighlightedItem;
var _84=0;
if(_83){
_84=_83.Index+1;
}
_84=this.FindNextAvailableIndex(_84);
if(_84<this.Items.length){
this.Items[_84].Highlight();
this.Items[_84].ScrollIntoView();
this.Items[_84].Text;
var _85=this.GetLastSeparatorIndex(this.GetText());
var _86=this.GetText().substring(0,_85+1)+this.Items[_84].Text;
this.SetText(_86);
this.SetValue(this.Items[_84].Value);
}
};
RadComboBox.prototype.HighlightPreviousItem=function(){
var _87=this.HighlightedItem;
var _88=0;
if(_87){
_88=_87.Index-1;
}
_88=this.FindPrevAvailableIndex(_88);
if(_88>=0){
this.Items[_88].Highlight();
this.Items[_88].ScrollIntoView();
this.Items[_88].Text;
var _89=this.GetLastSeparatorIndex(this.GetText());
var _8a=this.GetText().substring(0,_89+1)+this.Items[_88].Text;
this.SetText(_8a);
this.SetValue(this.Items[_88].Value);
}
};
RadComboBox.prototype.ElementOverflowsBottom=function(_8b,_8c,_8d){
var _8e=this.GetElementPosition(_8d).y+_8c.offsetHeight;
return _8e>_8b.height;
};
RadComboBox.prototype.FindItemByText=function(_8f){
for(var i=0;i<this.Items.length;i++){
if(this.Items[i].Text==_8f){
return this.Items[i];
}
}
return null;
};
RadComboBox.prototype.FindItemByValueInternal=function(_91){
if(!_91){
return null;
}
return this.FindItemByValue(_91);
};
RadComboBox.prototype.FindItemByValue=function(_92){
if(_92==null){
return null;
}
for(var i=0;i<this.Items.length;i++){
if(this.Items[i].Value==_92){
return this.Items[i];
}
}
return null;
};
RadComboBox.prototype.CancelPropagation=function(_94){
if(_94.stopPropagation){
_94.stopPropagation();
}else{
_94.cancelBubble=true;
}
};
RadComboBox.prototype.PreventDefault=function(_95){
if(_95.preventDefault){
_95.preventDefault();
}else{
_95.returnValue=false;
}
};
RadComboBox.prototype.GetText=function(){
return this.InputDomElement.value;
};
RadComboBox.prototype.Enable=function(){
this.EnableDomEventHandling();
this.InputDomElement.disabled=false;
this.Enabled=true;
};
RadComboBox.prototype.Disable=function(){
this.Enabled=false;
this.TextHidden.value=this.GetText();
this.InputDomElement.disabled="disabled";
this.DisableDomEventHandling();
};
RadComboBox.prototype.FixUp=function(_96,_97){
if((this.ClientWidthHidden.value!="")&&(this.ClientHeightHidden.value!="")){
if(_96.style.width!=this.ClientWidthHidden.value){
_96.style.width=this.ClientWidthHidden.value;
}
if(_96.style.height!=this.ClientHeightHidden.value){
_96.style.height=this.ClientHeightHidden.value;
}
this.ShowWrapperElement();
return;
}
var _98=_96.parentNode.getElementsByTagName("img")[0];
if(_97&&_98&&(_98.offsetWidth==0)){
var _99=this;
if(document.attachEvent){
if(document.readyState=="complete"){
window.setTimeout(function(){
_99.FixUp(_96,false);
},100);
}else{
window.attachEvent("onload",function(){
_99.FixUp(_96,false);
});
}
}else{
window.addEventListener("load",function(){
_99.FixUp(_96,false);
},false);
}
return;
}
var _9a=null;
if(_96.currentStyle){
_9a=_96.currentStyle;
}else{
if(document.defaultView&&document.defaultView.getComputedStyle){
_9a=document.defaultView.getComputedStyle(_96,null);
}
}
if(_9a==null){
this.ShowWrapperElement();
return;
}
var _9b=parseInt(_9a.height);
var _9c=parseInt(_96.offsetWidth);
var _9d=parseInt(_9a.paddingTop);
var _9e=parseInt(_9a.paddingBottom);
var _9f=parseInt(_9a.paddingLeft);
var _a0=parseInt(_9a.paddingRight);
var _a1=parseInt(_9a.borderTopWidth);
if(isNaN(_a1)){
_a1=0;
}
var _a2=parseInt(_9a.borderBottomWidth);
if(isNaN(_a2)){
_a2=0;
}
var _a3=parseInt(_9a.borderLeftWidth);
if(isNaN(_a3)){
_a3=0;
}
var _a4=parseInt(_9a.borderRightWidth);
if(isNaN(_a4)){
_a4=0;
}
if(document.compatMode&&document.compatMode=="CSS1Compat"){
if(!isNaN(_9b)&&(this.ClientHeightHidden.value=="")){
_96.style.height=_9b-_9d-_9e-_a1-_a2+"px";
this.ClientHeightHidden.value=_96.style.height;
}
}
if(!isNaN(_9c)&&_9c&&(this.ClientWidthHidden.value=="")){
var _a5=0;
if(_98){
_a5=_98.offsetWidth;
}
if(document.compatMode&&document.compatMode=="CSS1Compat"){
var _a6=_9c-_a5-_9f-_a0-_a3-_a4;
if(_a6>=0){
_96.style.width=_a6+"px";
}
this.ClientWidthHidden.value=_96.style.width;
}else{
_96.style.width=_9c-_a5;
}
}
this.ShowWrapperElement();
};
RadComboBox.prototype.ShowWrapperElement=function(){
if(!this.ShowWhileLoading){
document.getElementById(this.ClientID+"_wrapper").style.visibility="visible";
}
};;//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
