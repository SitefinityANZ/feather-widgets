﻿(function ($) {
    var submitButtonControllerTypeName = 'Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.SubmitButtonController';

    var findSubmitButtonDock = function (wrapperDockingZones) {
        if (wrapperDockingZones && wrapperDockingZones.length) {
            var allDocks = [];

            wrapperDockingZones.forEach(function (dockingZoneId) {
                allDocks.push.apply(allDocks, $find(dockingZoneId).get_docks());
            });

            var submitButtonDocks = allDocks.filter(function (dock) {
                var dockProperties = JSON.parse($(dock.get_element()).attr('parameters') || null);
                if (dockProperties && dockProperties.length) {
                    return dockProperties.some(function (prop) {
                        return prop.Key === 'ControllerName' && prop.Value === submitButtonControllerTypeName;
                    });
                }
            });

            return submitButtonDocks[0];
        }
    };

    $(function () {
        $(document).on('sf-zone-editor-item-dropped', function (e) {
            if (e && e.args && e.args.ControlType === 'Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy' && e.args.Attributes && e.args.Attributes.isLayoutControl !== 'True') {
                var droppedMvcFields = $('#RadDockZoneBody').find('[id^="RadDockClone"][behaviourobjecttype]');
                if (droppedMvcFields && droppedMvcFields.length) {
                    var hasSubmitButton = Array.prototype.some.call(droppedMvcFields, function (el) {
                        return el.attributes.behaviourobjecttype.value === submitButtonControllerTypeName;
                    });

                    if (!hasSubmitButton) {
                        var submitButtonDock = findSubmitButtonDock(e.sender.get_toolboxDockingZones());
                        e.sender._dropSubmitButton(submitButtonDock, e.args.DockId);
                    }
                }
            }
        });
    });
}(jQuery));