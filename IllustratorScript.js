doc = app.activeDocument;

//images are in this folder -yes, you need double "\\" for some reason I still don't know-
var dirQR = new Folder("C:\\Development\\Petzfinder\\Assets\\QRcodes");
var dirAssets = new Folder("C:\\Development\\Petzfinder\\Assets");
//gets images with .png extention
var imagesList = dirQR.getFiles('*.png');
//gets images .ai, apart from all the images so it can be used separately
var tagImage = dirAssets.getFiles('Tag.ai');
//gets images .ai, apart from all the images so it can be used separately
var pawImage = dirAssets.getFiles('Paw.ai');
//initializes counter
var layersDeleted = 0;
//foreach layer to remove all except background
for (var i = 0; i < app.documents.length; i++) {
  var targetDocument = app.documents[i];
  var layerCount = targetDocument.layers.length;

  // Loop through layers from the back, to preserve index
  // of remaining layers when we remove one
  for (var ii = layerCount - 1; ii >= 0; ii--) {
    var targetLayer = targetDocument.layers[ii];
    var layerName = new String(targetLayer.name);
    if (layerName.indexOf("Background") != 0) {
      targetDocument.layers[ii].remove();
      layersDeleted++;
    }
  }
}

//Default value for variables, defines starting positions
var startPositionX = 0;
var startPositionY = 0;
var startPositionText = 11.50394;

var tagHeight = 99.2126;
var tagWidth = 70.8661;

var docHeight = 3401.57;
var docWidth = 1700.79;

var tagPositionX = startPositionX;
var tagPositionY = startPositionY;

var imageTop = -14.6772;
var imageLeft = startPositionX;

var pawTop = -5.6772;
var pawLeft = 50;

var textTop = -82.2047;
var textLeft = startPositionText;
var codesLayer = doc.layers.add()
codesLayer.name = 'Codes';

for (var i = 0; i < imagesList.length; i++) {
    
    if(tagPositionX + tagWidth > docWidth){
        tagPositionX = startPositionX;
        textLeft = startPositionText;
        imageLeft = startPositionX;
        imageTop = imageTop - tagHeight;
        pawLeft = 50;
        pawTop = pawTop - tagHeight;
        textTop = textTop - tagHeight;
        tagPositionY = tagPositionY - tagHeight;
    }else if(tagPositionY + tagHeight > docHeight){
        break;
    }

    var imgName = imagesList[i].name;
    var documentName = doc.name;
    var imgNameNoExt = imgName.slice(0, imgName.indexOf("."));
    var docuNameNoExt = documentName.slice(0, documentName.indexOf("."));
    
    //QR Image object
    doc.activeLayer = codesLayer;
    var itemToPlace = doc.placedItems.add();
    itemToPlace.file = imagesList[i];
    itemToPlace.layer = codesLayer.name; //ToDo: not working
    itemToPlace.top = imageTop;
    itemToPlace.left = imageLeft;

    //QR Image object
    var pawToPlace = doc.placedItems.add();
    pawToPlace.file = pawImage[0];
    pawToPlace.layer = codesLayer.name; //ToDo: not working
    pawToPlace.top = pawTop;
    pawToPlace.left = pawLeft;

    //text frame object
    var txtFrame = doc.textFrames.add();
    txtFrame.contents = 'tag: '+ imgNameNoExt;
    txtFrame.selected = true;
    txtFrame.resize(85,70);

    //Assigns position to text frame
    var textArt = doc.textFrames[0];
    textArt.top = textTop;
    textArt.left = textLeft;

    //Adds the value to move right
    textLeft = textLeft + tagWidth
    imageLeft = imageLeft + tagWidth
    pawLeft = pawLeft + tagWidth
    tagPositionX = tagPositionX + tagWidth;
}

var tagsLayer = doc.layers.add()
tagsLayer.name = 'Tags';
tagPositionX = startPositionX;
tagPositionY = startPositionY;

for (var i = 0; i < imagesList.length; i++) {
    if(tagPositionX + tagWidth > docWidth){
        tagPositionX = startPositionX;
        tagPositionY = tagPositionY - tagHeight;
    }else if(tagPositionY + tagHeight > docHeight){
        break;
    }
    //Tag object
    doc.activeLayer = tagsLayer;
    var itemToPlaceTag = doc.placedItems.add();
    itemToPlaceTag.file = tagImage[0];
    itemToPlaceTag.layer = tagsLayer.name;
    itemToPlaceTag.top = tagPositionY;
    itemToPlaceTag.left = tagPositionX;

    tagPositionX = tagPositionX + tagWidth;
}