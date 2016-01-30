var STATE_ON = "stateOn";
var STATE_OFF = "stateOff";
var CONTAINER_ID = 'JSGRIDCONTAINER';
var ERROR_CONTAINER_ID = 'JSERRORCONTAINER';
var RULES_CONTAINER_ID = 'JSRULESCONTAINER';

var MATCH_UNKNOWN = -1;
var MATCH_INAPPLICABLE = 0;
var MATCH_FALSE = 1;
var MATCH_TRUE = 2;

var CELL_MATCH_NONE = 0;
var CELL_MATCH_CONDITION = 1;
var CELL_MATCH_ACTION = 2;

var data = {
    imageSize: 80,
    ruleImageSize: 200,
    grid: {
        width: 10,
        height: 6
    },

    states: [
        {
            id:STATE_ON,
            ruleID: 2
        },{
            id:STATE_OFF,
            ruleID: 1
        }
    ],
    blocks: [
        {
            ruleCondition: "r",
            ruleAction:"R",
            stateOff: "img/rect1.png",
            stateOn: "img/rect2.png"
        },{
            ruleCondition: "c",
            ruleAction:"C",
            stateOff: "img/circle1.png",
            stateOn: "img/circle2.png"
        },{
            ruleCondition: "t",
            ruleAction:"T",
            stateOff: "img/triangle1.png",
            stateOn: "img/triangle2.png"
        }
    ],
    rules: [
        {
            image: "img/ruleRectOverCircle.png",
            pattern: [
                [ "R2" ],
                [ "C2" ]
            ]
        },{
            image: "img/ruleTriangleOnLeft.png",
            pattern: [
                [ "T2", "T2" ]
            ]
        }, {
            image:"img/ruleTriangleOffNextToRectOn.png",
            pattern: [
                [ "T1", "R2" ]
            ]
        }
    ]
};


var grid = {

    images: [],
    // Keys are the result of the coordsString() function.
    imagesByPosition: {},


    activeRules: [],

    // Helper function so I don't have to write for for for a million times.
    iterateOverGrid: function(callback, callbackAfterRow, thisArg) {
        _.times(data.grid.height, (function(heightIndex){

            _.times(data.grid.width, (function(widthIndex){
                callback.bind(thisArg)(widthIndex, heightIndex);
            }).bind(this));

            if (callbackAfterRow) callbackAfterRow.bind(thisArg)(heightIndex);

        }).bind(this));
    },
    getImageAtPosition: function(widthIndex, heightIndex) {
        var string = this.stringFromCoords(widthIndex, heightIndex);
        return this.imagesByPosition[string];
    },
    stringFromCoords: function(widthIndex, heightIndex) {
        return widthIndex + "-" + heightIndex;
    },
    coordsFromString: function(coordsString) {
        return {
            x: Number(coordsString.match(/^\d+/)[0]),
            y: Number(coordsString.match(/\d+$/)[0])
        }
    },

    setup: function() {
        this.images = [];
        this.imagesByPosition = {};

        var container = document.getElementById(CONTAINER_ID);
        container.innerHTML = "";
        this.iterateOverGrid(function(widthIndex, heightIndex){
            this.addRandomImage(container, widthIndex, heightIndex);
        }, function(heightIndex) {
            this.addLineReset(container);
        }, this);

        var amountOfRules = Math.random() < 0.1 ? 2 : 1;
        this.activeRules = _.sampleSize(data.rules, amountOfRules);

        var rulesContainer = document.getElementById(RULES_CONTAINER_ID);
        rulesContainer.innerHTML = '';
        _.each(this.activeRules, (function(rule) {
            this.addRuleImage(rulesContainer, rule);
        }).bind(this));
        console.log(JSON.stringify(this.activeRules));

        this.checkRules();
    },
    addLineReset: function(container) {
        var clearElement = document.createElement("div");
        clearElement.style.clear = "both";
        container.appendChild(clearElement);
    },
    addRandomImage: function(container, widthIndex, heightIndex) {
        var block = data.blocks[ Math.floor(Math.random() * data.blocks.length) ];
        var state = data.states[ Math.floor(Math.random() * data.states.length) ];

        var image = new Image(block, state, data.imageSize);
        this.images.push(image);
        var string = this.stringFromCoords(widthIndex, heightIndex);
        this.imagesByPosition[string] = image;

        image.addToContainer(container);
    },
    addRuleImage: function(container, rule) {
        var element = document.createElement("img");
        element.className += "ruleimage";
        element.setAttribute("src", rule.image);
        element.setAttribute("width", "" + data.ruleImageSize);
        element.setAttribute("height", "" + data.ruleImageSize);
        container.appendChild(element);
    },


    checkRules: function() {
        console.log("======== checking rules");
        var totalErrorCount = _.map(this.activeRules, (function(rule){
            return this.checkSingleRule(rule);
        }).bind(this));

        var total = 0;
        _.forEach(totalErrorCount, function(value){
            total += value;
        });
        document.getElementById(ERROR_CONTAINER_ID).innerHTML = total + " errors";
        console.log(total);

        if (total === 0) {
            this.setup();
        }

    },
    checkSingleRule: function(rule) {
        var errorCount = 0;
        this.iterateOverGrid(function(widthIndex, heightIndex){
            var result = this.checkRuleForAnchorPoint(widthIndex, heightIndex, rule);
            if (result === MATCH_FALSE) errorCount++;
        }, undefined, this);
        return errorCount;
    },

    /*  Returns:
        - 0 if the rule is not applicable to this tile.
        - 1 if the rule is applicable and applied wrong.
        - 2 if the rule is applicable and a perfect match with the expected setup.
     */
    checkRuleForAnchorPoint: function(widthIndex, heightIndex, rule) {

        /*  voor elk vakje:
                 kijk of het gridvakje matcht.
                 kijk of het element overeenkomt.
                 kijk of de state overeenkomt als die staat aangegeven.
                 op de eerste nonmatch, return 0.

            voor elk vakje dat een action is:
                 kijk of de state matcht.
                 op een enkele nonmatch, return 1.

            return 2
         */
        var matchResult = MATCH_UNKNOWN;
        var ruleWidthIndex;
        var ruleHeightIndex;

        // First, check the rule tiles for matching shapes and potentially return MATCH_INAPPLICABLE.
        for (var i = 0; i < rule.pattern.length; i++) {
            var row = rule.pattern[i];
            ruleHeightIndex = i;
            for (var j = 0; j < row.length; j++) {
                var cell = row[j];
                ruleWidthIndex = j;

                var coords = {
                    width: widthIndex + ruleWidthIndex,
                    height: heightIndex + ruleHeightIndex
                };
                var image = this.getImageAtPosition(coords.width, coords.height);
                if (image === undefined) {
                    matchResult = MATCH_INAPPLICABLE;
                    break;
                }
                var matchType = this.getMatchForCellAndImageBlock(cell, image);
                if (matchType === CELL_MATCH_NONE) {
                    matchResult = MATCH_INAPPLICABLE;
                    break;
                } else if (matchType === CELL_MATCH_CONDITION) {
                    if (this.getMatchForCellAndImageState(cell, image) === false) {
                        matchResult = MATCH_INAPPLICABLE;
                        break;
                    }
                }
            }
        }
        if (matchResult === MATCH_INAPPLICABLE) {
            return matchResult;
        }

        // From here, the match can only be MATCH_TRUE or MATCH_FALSE
        matchResult = MATCH_TRUE;

        for (var i = 0; i < rule.pattern.length; i++) {
            var row = rule.pattern[i];
            ruleHeightIndex = i;
            for (var j = 0; j < row.length; j++) {
                var cell = row[j];
                ruleWidthIndex = j;
                var coords = {
                    width: widthIndex + ruleWidthIndex,
                    height: heightIndex + ruleHeightIndex
                };

                var image = this.getImageAtPosition(coords.width, coords.height);
                if (this.getMatchForCellAndImageBlock(cell, image) === CELL_MATCH_ACTION) {
                    if (this.getMatchForCellAndImageState(cell, image) === false) {
                        matchResult = MATCH_FALSE;
                        break;
                    }
                }
            }
        }
        if (matchResult === MATCH_FALSE) {
            console.log("==== " + this.stringFromCoords(widthIndex, heightIndex));
            console.log(matchResult);
        }
        return matchResult;
    },
    getMatchForCellAndImageBlock: function(cellString, image) {
        var ruleID = cellString.substring(0,1);
        if (_.isEqual(ruleID, image.block.ruleCondition)) {
            return CELL_MATCH_CONDITION;
        } else if (_.isEqual(ruleID, image.block.ruleAction)) {
            return CELL_MATCH_ACTION;
        } else {
            return CELL_MATCH_NONE;
        }
    },
    getMatchForCellAndImageState: function(cellString, image) {
        var stateID = "" + cellString.substring(1,2);
        if (_.isEqual(stateID, "" + 0)) {
            return true;
        } else {
            if (_.isEqual(stateID, "" + image.state.ruleID)) {
                return true;
            } else {
                return false;
            }
        }
    }

};

function Image(block, initialState, imageSize) {
    var that = {
        block: block,
        state: initialState,
        element: null,
        addToContainer: function(container) {
            container.appendChild(that.element);
        },
        elementClicked: function(event) {
            var indexOfCurrentState = _.findIndex(data.states, function(value, index){
                return value === that.state;
            });
            var newIndex = indexOfCurrentState + 1;
            if (newIndex >= data.states.length) newIndex = 0;

            that.state = data.states[newIndex];
            that.element.setAttribute("src", that.block[that.state.id]);

            grid.checkRules();
        }
    };

    // Creates the element. NOTE: this element must be attached to a container later.
    var element = document.createElement("img");
    that.element = element;

    element.className += "gridimage";
    element.setAttribute("src", block[that.state.id]);
    element.setAttribute("width", "" + imageSize);
    element.setAttribute("height", "" + imageSize);
    element.addEventListener("click", that.elementClicked);

    return that;
}

// Initialises our grid.
grid.setup();