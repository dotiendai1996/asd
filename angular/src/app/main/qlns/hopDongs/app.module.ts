import { NgModule, Directive, Input } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { coerceBooleanProperty } from '@angular/cdk/coercion';


@Directive({
  selector: '[readonly],[readOnly]',
  host: {
    '[attr.readonly]': '_isReadonly ? "" : null'
  }
})
class ReadonlyDirective {
  _isReadonly = false;

  @Input() set readonly (v) {
     this._isReadonly = coerceBooleanProperty(v);
  };

  ngOnChanges(changes) {
    console.log(changes);
  }
}

