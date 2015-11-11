'use strict';
app.controller('mealsListController', ['$scope', '$filter', '$timeout', 'navigationService', 'meals', 'mealsService', 'userSettings', 'mealsFilterService',
    function ($scope, $filter, $timeout, navigationService, meals, mealsService, userSettings, mealsFilterService) {
        navigationService.currentPath = ['meals'];
        $scope.meals = meals;
        $scope.addOrEdit = false;
        $scope.newMeal = {
            id: null,
            time: new Date(),
            caloriesConsumed: null
        };
        $scope.addingMessage = null;
        $scope.isError = false;
        $scope.isSuccess = false;
        $scope.nothingFound = false;
        $scope.errorMessages = [];
        $scope.userSettings = userSettings;
        $scope.totalForToday = 0;
        $scope.isExceeded = null;
        $scope.calorieAmountMessage = null;
        $scope.filter = {
            startDate: moment().format('YYYY-MM-DD'),
            endDate: moment().format('YYYY-MM-DD'),
            fromTime: moment(),
            toTime: moment(),
            filterOption: 'all'
        };


        if ($scope.meals.$promise != null) {
            $scope.meals.$promise.then(function () {
                $scope.getAllCaloriesEnteredForToday();
                $scope.validateExceedingOfCalorieAmounts();
            });
        }

        $scope.open = function ($event, opened) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope[opened] = true;
        };

        $scope.new = function () {
            $scope.isError = false;
            $scope.isSuccess = false;
            $scope.newMeal = {
                id: null,
                time: moment(),
                caloriesConsumed: 0,
                date: moment().format('YYYY-MM-DD')
            };
            $scope.addOrEdit = true;
        };

        $scope.cancelEditOrAdd = function () {
            $scope.addOrEdit = false;
        };

        $scope.getAllCaloriesEnteredForToday = function () {
            var closure = [];
            var total = 0;
            var todaysMeals = _.filter($scope.meals, function (meal) {
                return moment(moment().format('YYYY-MM-DD')).isSame(moment(meal.date).format('YYYY-MM-DD'));
            });

            this.total = 0;
            this.execute = function () {
                angular.forEach(todaysMeals, function (value, key) {
                    this.total = this.total + value.caloriesConsumed;
                }, this);
            }
            this.execute();
            $scope.totalForToday = this.total;
        };

        $scope.getAllMeals = function () {
            if ($scope.filter.filterOption === 'all') {
                $scope.meals = mealsService.query();
                $scope.nothingFound = false;
            }
        };

        $scope.validateExceedingOfCalorieAmounts = function () {
            if ($scope.userSettings.$promise != null) {
                $scope.userSettings.$promise.then(function () {
                    if ($scope.userSettings[0] === undefined) { return; }
                    if ($scope.userSettings[0].dailyCalories >= $scope.totalForToday) {
                        $scope.isExceeded = false;
                        $scope.calorieAmountMessage = "You have still not exceeded your daily calorie amount which is: {0}, you can keep eating  :)".format($scope.userSettings[0].dailyCalories);
                    }
                    else {
                        $scope.isExceeded = true;
                        $scope.calorieAmountMessage = "You have exceeded your daily calorie amount which is: {0} and you should probably stop !!!".format($scope.userSettings[0].dailyCalories);
                    }
                });
            }
        }

        String.prototype.format = function () {
            var formatted = this;
            for (var i = 0; i < arguments.length; i++) {
                var regexp = new RegExp('\\{' + i + '\\}', 'gi');
                formatted = formatted.replace(regexp, arguments[i]);
            }
            return formatted;
        };

        $scope.filterMeals = function () {
            $scope.meals = mealsFilterService.query({
                startDate: moment($scope.filter.startDate).format('YYYY-MM-DD'),
                endDate: moment($scope.filter.endDate).format('YYYY-MM-DD'),
                fromTime: moment($scope.filter.fromTime).format('YYYY-MM-DD HH:mm'),
                toTime: moment($scope.filter.toTime).format('YYYY-MM-DD HH:mm'),
            });

            if ($scope.meals.$promise != null) {
                $scope.meals.$promise.then(function () {
                    if ($scope.meals.length === 0) {
                        $scope.nothingFound = true;
                    }
                });
            }
        }
        

        $scope.save = function () {
            var savePromise = null;
            if ($scope.newMeal.id !== null) {
                savePromise = mealsService.update({
                    id: $scope.newMeal.id
                }, $scope.newMeal).$promise;
            } else {
                savePromise = mealsService.save($scope.newMeal).$promise;
            }

            savePromise.then(function (response) {
                $scope.meals = mealsService.query();
                $scope.addOrEdit = false;
                $scope.isSuccess = true;
                $scope.isError = false;
                $scope.addingMessage = "Success! Well done you have added a tasty meal.";
                if ($scope.meals.$promise != null) {
                    $scope.meals.$promise.then(function () {
                        $scope.getAllCaloriesEnteredForToday();
                        $scope.validateExceedingOfCalorieAmounts();
                    });
                }
            },
             function (error) {
                 $scope.isError = false;
                 $scope.errorMessages = [];
                 for (var key in error.data.modelState) {
                     if (error.data.modelState.hasOwnProperty(key)) {
                         for (var i = 0; i < error.data.modelState[key].length; i++) {
                             $scope.errorMessages.push(error.data.modelState[key][i]);
                         }
                     }
                 }
             });
        };

        $scope.edit = function (meal) {
            $scope.isError = false;
            $scope.isSuccess = false;

            $scope.addOrEdit = true;

            var timeOfDay = meal.time.split(":", 3);
            var hours = timeOfDay[0];
            var minutes = timeOfDay[1];
            var seconds = timeOfDay[2];

            $scope.newMeal = meal;
            $scope.newMeal.time = new Date();
            $scope.newMeal.time.setHours(hours);
            $scope.newMeal.time.setHours(hours, minutes, seconds, 0);
        };

        $scope.delete = function (meal) {
            $scope.getAllCaloriesEnteredForToday();
            mealsService.delete({ id: meal.id }, function () {
                $scope.meals = mealsService.query();
                if ($scope.meals.$promise != null) {
                    $scope.meals.$promise.then(function () {
                        $scope.getAllCaloriesEnteredForToday();
                        $scope.validateExceedingOfCalorieAmounts();
                    });
                }
            }, function () {
                $scope.message = 'Unable to delete meal';
            });
        };
        $scope.options = {
            step: 5,
            timeFormat: 'H:i'
        };
    }]);