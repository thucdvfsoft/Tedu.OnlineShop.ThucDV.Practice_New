/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '../../CKFinderScripts/ckfinder.html';
    config.filebrowserImageBrowseUrl = '../../CKFinderScripts/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '../../CKFinderScripts/ckfinder.html?Type=Flash';
    config.filebrowserImageUploadUrl = '../../MyFiles/files';

    CKFinder.setupCKEditor(null, '../../CKFinderScripts')
};
