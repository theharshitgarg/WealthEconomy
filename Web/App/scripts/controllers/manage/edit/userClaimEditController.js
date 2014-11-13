//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

(function () {
    'use strict';

    var controllerId = 'userClaimEditController';
    angular.module('main')
        .controller(controllerId, ['userClaimService',
            'userService',
            'logger',
            '$location',
            '$routeParams',
            userClaimEditController]);

    function userClaimEditController(userClaimService,
		userService,
		logger,
		$location,
		$routeParams) {
        logger = logger.forSource(controllerId);

        var isNew = $location.path() === '/manage/userClaim/new';
        var isSaving = false;

        // Controller methods (alphabetically)
        var vm = this;
        vm.userSet = [];
        vm.cancelChanges = cancelChanges;
        vm.isSaveDisabled = isSaveDisabled;
        vm.entityErrors = [];
        vm.userClaim = null;
        vm.saveChanges = saveChanges;
        vm.hasChanges = hasChanges;

        initialize();

        /*** Implementations ***/

        function cancelChanges() {

            $location.path('/manage/userClaim');

            if (userClaimService.hasChanges()) {
                userClaimService.rejectChanges();
                logWarning('Discarded pending change(s)', null, true);
            }
        }

        function hasChanges() {
            return userClaimService.hasChanges();
        }

        function initialize() {

            userService.getUserSet(false)
                .then(function (data) {
                    vm.userSet = data;
                });

            if (isNew) {
                // TODO For development enviroment, create test entity?
            }
            else {
                userClaimService.getUserClaim($routeParams.Id)
                    .then(function (data) {
                        vm.userClaim = data;
                    })
                    .catch(function (error) {
                        // TODO User-friendly message?
                    });
            }
        };

        function isSaveDisabled() {
            return isSaving ||
                (!isNew && !userClaimService.hasChanges());
        }

        function saveChanges() {

            if (isNew) {
                userClaimService.createUserClaim(vm.userClaim);
            } else {
                // To be able to do concurrency check, RowVersion field needs to be send to server
				// Since breeze only sends the modified fields, a fake modification had to be applied to RowVersion field
                var rowVersion = vm.userClaim.RowVersion;
                vm.userClaim.RowVersion = '';
                vm.userClaim.RowVersion = rowVersion;
            }

            isSaving = true;
            userClaimService.saveChanges()
                .then(function (result) {
                    $location.path('/manage/userClaim');
                })
                .catch(function (error) {
                    // Conflict (Concurrency exception)
                    if (error.status !== 'undefined' && error.status === '409') {
                        // TODO Try to recover!
                    } else if (error.entityErrors !== 'undefined') {
                        vm.entityErrors = error.entityErrors;
                    }
                })
                .finally(function () {
                    isSaving = false;
                });
        }
    };
})();
