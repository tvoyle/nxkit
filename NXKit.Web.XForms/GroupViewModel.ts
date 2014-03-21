﻿/// <reference path="XFormsVisualViewModel.ts" />

module NXKit.Web.XForms {

    export module GroupViewModel_ {

        /**
          * Represents a sub-item of a top-level group.
          */
        export class Item {

            private _viewModel: GroupViewModel;
            private _level: number;

            constructor(viewModel: GroupViewModel, level: number) {
                this._viewModel = viewModel;
                this._level = level;
            }

            public get ViewModel(): GroupViewModel {
                return this._viewModel;
            }

            public get Level(): number {
                return this._level;
            }

            public get Relevant(): KnockoutObservable<boolean> {
                return this.GetRelevant();
            }

            GetRelevant(): KnockoutObservable<boolean> {
                return ko.computed<boolean>(() => true);
            }

            public get ReadOnly(): KnockoutObservable<boolean> {
                return this.GetReadOnly();
            }

            GetReadOnly(): KnockoutObservable<boolean> {
                return ko.computed<boolean>(() => false);
            }

            public get Required(): KnockoutObservable<boolean> {
                return this.GetRequired();
            }

            GetRequired(): KnockoutObservable<boolean> {
                return ko.computed<boolean>(() => false);
            }

            public get Valid(): KnockoutObservable<boolean> {
                return this.GetValid();
            }

            GetValid(): KnockoutObservable<boolean> {
                return ko.computed<boolean>(() => true);
            }

            public get Label() {
                return this.GetLabel();
            }

            GetLabel(): Visual {
                throw new Error('GetLabel not implemented');
            }

            public get Help() {
                return this.GetHelp();
            }

            GetHelp(): Visual {
                throw new Error('GetHelp not implemented');
            }

            public get Layout() {
                return this.GetLayout();
            }

            GetLayout(): any {
                throw new Error('GetLayout not implemented');
            }

        }

        /**
          * Describes an item that will render a raw visual.
          */
        export class VisualItem
            extends Item {

            private _itemVisual: Visual;

            constructor(viewModel: GroupViewModel, itemVisual: Visual, level: number) {
                super(viewModel, level);
                this._itemVisual = itemVisual;
            }

            public get ItemVisual(): Visual {
                return this._itemVisual;
            }

            GetRelevant(): KnockoutObservable<boolean> {
                return Utils.GetRelevant(this._itemVisual);
            }

            GetReadOnly(): KnockoutObservable<boolean> {
                return Utils.GetReadOnly(this._itemVisual);
            }

            GetRequired(): KnockoutObservable<boolean> {
                return Utils.GetRequired(this._itemVisual);
            }

            GetValid(): KnockoutObservable<boolean> {
                return Utils.GetValid(this._itemVisual);
            }

            GetLabel(): Visual {
                if (this._itemVisual.Type == 'NXKit.XForms.XFormsInputVisual' &&
                    Utils.GetType(this._itemVisual)() == '{http://www.w3.org/2001/XMLSchema}boolean')
                    // boolean inputs provide their own label
                    return null;
                else
                    return Utils.GetLabel(this._itemVisual);
            }

            GetHelp(): Visual {
                return Utils.GetHelp(this._itemVisual);
            }

            GetLayout(): any {
                return {
                    visual: this.ViewModel.Visual,
                    data: this,
                    layout: 'visual',
                    level: this.Level,
                };
            }

        }

        export class InputItem
            extends VisualItem {

            constructor(viewModel: GroupViewModel, inputVisual: Visual, level: number) {
                super(viewModel, inputVisual, level);
            }

            public get InputVisual(): Visual {
                return this.ItemVisual;
            }

            GetLayout(): any {
                return {
                    visual: this.ViewModel.Visual,
                    data: this,
                    layout: 'input',
                    type: Utils.GetType(this.InputVisual),
                    level: this.Level,
                };
            }

        }

        /**
          * Describes a sub-item of a top-level group that will render a single underlying item.
          */
        export class SingleItem
            extends Item {

            private _item: Item;
            private _force: boolean;

            constructor(viewModel: GroupViewModel, level: number) {
                super(viewModel, level);
            }

            public get Item(): Item {
                return this._item;
            }

            public set Item(item: Item) {
                this._item = item;
            }

            public get Force() {
                return this._force;
            }

            public set Force(force: boolean) {
                this._force = force;
            }

            GetRelevant(): KnockoutObservable<boolean> {
                return this.Item.Relevant;
            }

            GetReadOnly(): KnockoutObservable<boolean> {
                return this.Item.ReadOnly;
            }

            GetRequired(): KnockoutObservable<boolean> {
                return this.Item.Required;
            }

            GetValid(): KnockoutObservable<boolean> {
                return this.Item.Valid;
            }

            GetLabel(): Visual {
                return this._item.Label;
            }

            GetHelp(): Visual {
                return this._item.Help;
            }

            GetLayout(): any {
                return {
                    visual: this.ViewModel.Visual,
                    data: this,
                    layout: 'single',
                    level: this.Level,
                }
            }

        }

        /**
          * Describes a sub-item of a top-level group which will render two items.
          */
        export class DoubleItem
            extends Item {

            private _item1: Item;
            private _item2: Item;

            constructor(viewModel: GroupViewModel, level: number) {
                super(viewModel, level);
            }

            public get Item1(): Item {
                return this._item1;
            }

            public set Item1(item: Item) {
                this._item1 = item;
            }

            public get Item2(): Item {
                return this._item2;
            }

            public set Item2(item: Item) {
                this._item2 = item;
            }

            GetRelevant(): KnockoutObservable<boolean> {
                return ko.computed(() => this._item1.Relevant() && this._item2.Relevant());
            }

            GetReadOnly(): KnockoutObservable<boolean> {
                return ko.computed(() => this._item1.ReadOnly() && this._item2.ReadOnly());
            }

            GetRequired(): KnockoutObservable<boolean> {
                return ko.computed(() => this._item1.Required() && this._item2.Required());
            }

            GetValid(): KnockoutObservable<boolean> {
                return ko.computed(() => this._item1.Valid() && this._item2.Valid());
            }

            GetLabel(): Visual {
                return null;
            }

            GetHelp(): Visual {
                return null;
            }

            GetLayout(): any {
                return {
                    visual: this.ViewModel.Visual,
                    data: this,
                    layout: 'double',
                    level: this.Level,
                }
            }

        }

        export class GroupItem
            extends Item {

            private _groupVisual: Visual;
            private _items: Item[];

            constructor(viewModel: GroupViewModel, groupVisual: Visual, level: number) {
                super(viewModel, level);
                this._groupVisual = groupVisual;
                this._items = new Array<Item>();
            }

            get Items(): Item[] {
                return this._items;
            }

            set Items(items: Item[]) {
                this._items = items;
            }

            GetRelevant(): KnockoutObservable<boolean> {
                return Utils.GetRelevant(this._groupVisual);
            }

            GetReadOnly(): KnockoutObservable<boolean> {
                return Utils.GetReadOnly(this._groupVisual);
            }

            GetRequired(): KnockoutObservable<boolean> {
                return Utils.GetRequired(this._groupVisual);
            }

            GetValid(): KnockoutObservable<boolean> {
                return Utils.GetValid(this._groupVisual);
            }

            GetLabel(): Visual {
                return Utils.GetLabel(this._groupVisual);
            }

            GetHelp(): Visual {
                return Utils.GetHelp(this._groupVisual);
            }

            GetLayout(): any {
                return {
                    visual: this.ViewModel.Visual,
                    data: this,
                    layout: 'group',
                    level: this.Level,
                };
            }

        }

    }

    export class GroupViewModel
        extends XFormsVisualViewModel {

        private _count: number;

        constructor(context: KnockoutBindingContext, visual: Visual, count: number) {
            super(context, visual);

            this._count = count;
        }

        /**
         * Gets the set of contents expressed as template binding objects.
         */
        public get BindingContents(): GroupViewModel_.Item[] {
            return this.GetBindingContents();
        }

        private GetBindingContents(): GroupViewModel_.Item[] {
            return this.GetItems(this.Visual, 1);
        }

        /**
         * Gets the set of contents expressed as template binding objects.
         */
        private GetGroupItem(visual: Visual, level: number): GroupViewModel_.GroupItem {
            var item = new GroupViewModel_.GroupItem(this, visual, level);
            item.Items = this.GetItems(visual, level + 1);
            return item;
        }

        private GetItems(visual: Visual, level: number): GroupViewModel_.Item[] {
            var list = new Array<GroupViewModel_.Item>();
            var cnts = Utils.GetContents(visual);
            for (var i = 0; i < cnts.length; i++) {
                var v = cnts[i];

                // nested group obtains single child
                if (v.Type == 'NXKit.XForms.XFormsGroupVisual') {
                    var groupItem = this.GetGroupItem(v, level);
                    list.push(groupItem);
                    continue;
                } else if (v.Type == 'NXKit.XForms.XFormsTextAreaVisual') {
                    var textAreaItem = new GroupViewModel_.SingleItem(this, level);
                    textAreaItem.Force = true;
                    list.push(textAreaItem);
                    continue;
                }

                // check if last inserted item was a single item, if so, replace with a double item
                var item = list.pop();
                if (item instanceof GroupViewModel_.SingleItem && !(<GroupViewModel_.SingleItem>item).Force) {
                    var item1 = <GroupViewModel_.SingleItem>item;
                    var item2 = new GroupViewModel_.DoubleItem(this, level);
                    item2.Item1 = item1.Item;
                    item2.Item2 = new GroupViewModel_.VisualItem(this, v, level);
                    list.push(item2);
                }
                else {
                    // put previous item back into list
                    if (item != null)
                        list.push(item);

                    // insert new single item
                    var item1 = new GroupViewModel_.SingleItem(this, level);
                    item1.Item = new GroupViewModel_.VisualItem(this, v, level);
                    list.push(item1);
                }
            }

            return list;
        }

    }

}