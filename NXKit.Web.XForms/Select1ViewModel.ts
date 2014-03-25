﻿/// <reference path="XFormsNodeViewModel.ts" />

module NXKit.Web.XForms {

    export class Select1ViewModel
        extends NXKit.Web.XForms.XFormsNodeViewModel {

        public static GetItems(node: Node) {
            return ko.utils.arrayFilter(node.Nodes(), (_: Node) => {
                return _.Type == 'NXKit.XForms.ItemElement';
            });
        }

        public static GetItemValue(node: Node) {
            return ko.utils.arrayFirst(node.Nodes(), (_) => {
                return _.Type == 'NXKit.XForms.ValueElement';
            });
        }

        public static GetSelectedItemNodeId(node: Node): KnockoutComputed<string> {
            return ko.computed<string>({
                read: () => {
                    if (node != null &&
                        node.Properties['SelectedItemNodeId'] != null)
                        return node.Properties['SelectedItemNodeId'].ValueAsString();
                    else
                        return null;
                },
                write: _ => {
                    if (node != null &&
                        node.Properties['SelectedItemNodeId'] != null)
                        node.Properties['SelectedItemNodeId'].Value(_);
                },
            });
        }

        constructor(context: KnockoutBindingContext, node: Node) {
            super(context, node);
        }

        public get Items(): Node[] {
            return Select1ViewModel.GetItems(this.Node);
        }

        public get SelectedItemNodeId(): KnockoutComputed<string> {
            return Select1ViewModel.GetSelectedItemNodeId(this.Node);
        }

    }

}