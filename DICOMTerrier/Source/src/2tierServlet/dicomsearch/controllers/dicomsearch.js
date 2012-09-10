/**
* This Javascript contains the functions that are needed 
* in the DICOM search webinterface.
*
* Author Gerald van Veldhuijsen
*/

/*************************
* Basic Screen Functions *
**************************/

//Variable declarations
Image1= new Image(150,150);
Image1.src = '\/images\/onemomentplease.gif' ;
fullimage = new Image();
var fullimagepath;
var viewlarge = false;
var windownew;
var link = "preview0";

//Function to enable 'enter' for submitting input
function catchEnter(e) { 
 	if(e.keyCode == 13){ 
  		document.getForm.submit.value='submit'; 
  		document.getForm.submit();
 	} 
} 

//Function to change the preview picture
function change(picture, linkName) {
	link = linkName;
	//window.alert("Active link " + link);
	viewlarge = false;
 	document.preview.src= '\/images\/onemomentplease.gif'; 
 	document.preview.src= 'showimage?filename=' + picture; 
 	fullimagepath= 'showimage?filename=' + picture + '&size=full';
} 

//Function to open the picture in a new window
function newwindow(){	
	if(fullimagepath){
		var a = document.getElementsByTagName("a");
		for(var i=0; i < a.length; i++) {
			if (a[i].getAttribute('name') == link){
				a[i].style.color="CC0033";				
			}
			else
				a[i].style.color="666666";				
		}	  	
	  
	  	viewlarge =true;
  		fullimage.src = fullimagepath;
  		width = fullimage.width;
  		height = fullimage.height;
  		windownew = window.open(fullimage.src,'jav','scrollbars=1, width=' + (width+ 40) + ',height=' + (height+50) + ',resizable=yes'); 
		windownew.document.write('<html><body bgcolor=black><center><img src=' + fullimage.src + ' name=full ><br><a href="javascript:self.close()" ><font color=white><b>Close this window</b><\/a><\/center><\/body><\/html>');
  		windownew.focus();windownew.moveTo(0,0);
  		document.preview.src = fullimagepath;
 	}
}

//Function to resize the image window after image is loaded
function checkLarge(e){
 	if (viewlarge){
  		width = fullimage.width;
		height = fullimage.height;
  		if(screen.width && screen.height){
	   		if(width>(screen.width-40))
    			width=screen.width-40; 
    		if (height>(screen.height-100))
     			height=screen.height-100; 
  		} else {
	   		width=1024-40;
	   		height=768-65;
	   	}
  		windownew.resizeTo(width+50, height+70);
 	}
}

/****************************
* Advanced Screen Functions *
*****************************/

//Function to handle the submit in the advanced screen
function dosubmit (value) { 
	document.frm.moreFields.value = value;
	document.frm.AdvancedQuery.value = 'advanced';
	document.frm.submit();
}