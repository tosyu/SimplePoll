import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { PageNotFoundComponent } from "./components/PageNotFoundComponent/page-not-found.component";
import { environment } from "src/environments/environment";

const appRoutes: Routes = [
  { path: '', redirectTo: '/polls-list', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      {
        enableTracing: environment.pathTracing ?? false
      }
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutesModule { }
